using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;
		public static GameManager Instance { get { return _instance; } }

		private Vector3 view;
		private float SpawnWait = 1f;
		private float WaveWait = 5f;
		private int AsteroidCount = 20;
		private float AsteroidSpeed = 1f;
		private Camera MainCamera;

		public GameObject Asteroid;
		public GameObject Fuel;
		public GameObject Bullet;
		public PlayerController Player;
		public Transform Background;

		void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
			}

			MainCamera = Camera.main;
			view = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane));
			Background.localScale = new Vector3(view.x * 2f + 2f, view.x * 2f + 2f, 0);
		}

		private void Start()
		{
			StartCoroutine(Spawn());
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}

		internal void SpawnWeapon()
		{
			var x = Random.Range(-view.x, view.x);
			var y = Random.Range(-view.y, view.y);
			Instantiate(Bullet, new Vector3(x, y, 0), Quaternion.identity);
		}

		internal void SpawnFuel()
		{
			var x = Random.Range(-view.x, view.x);
			var y = Random.Range(-view.y, view.y);
			Instantiate(Fuel, new Vector3(x, y, 0), Quaternion.identity);
		}

		private IEnumerator Spawn()
		{
			yield return new WaitForSeconds(SpawnWait);
			while(true)
			{
				for (int i = 0; i < AsteroidCount; i++)
				{
					float x = 0f;
					float y = 0f;

					if (Random.Range(0, 100) > 50)
					{
						x = Random.Range(0, 100) > 50 ? -view.x - 1 : view.x + 1;
						y = Random.Range(-view.y, view.y);
					}
					else
					{
						x = Random.Range(-view.x, view.x);
						y = Random.Range(0, 100) > 50 ? -view.y - 1 : view.y + 1;
					}

					var pos = new Vector3(x + MainCamera.transform.position.x, y + MainCamera.transform.position.y, 0);
					var dir = Player.transform.position - pos;
					var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

					var obj = Instantiate(Asteroid, pos, Quaternion.Euler(0, 0, angle));
					obj.GetComponent<AsteroidController>().speed = AsteroidSpeed;
					yield return new WaitForSeconds(SpawnWait);
				}
				yield return new WaitForSeconds(WaveWait);
				AsteroidCount++;
				AsteroidSpeed = AsteroidSpeed + 0.4f;
			}
		}
	}
}
