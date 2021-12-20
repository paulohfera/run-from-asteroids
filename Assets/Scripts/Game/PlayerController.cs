using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game
{
	public class PlayerController : MonoBehaviour
	{
		private float xBound, yBound;
		private Camera MainCamera;
		private HUDManager Hud;

		public float speed = 5;
		public float linearDrag = 0.5f;
		public int Bullets = 50;
		public int Fuel = 50;

		public Transform Weapon;
		public GameObject Bullet;

		private Rigidbody2D rb;

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			rb.drag = linearDrag;

			MainCamera = Camera.main;

			InvokeRepeating("DecreaseGas", 5f, 5f);

			HUDManager.instance.UpdateBullets(Bullets);
			HUDManager.instance.UpdateFuel(Fuel);
			var bounds = HUDManager.instance.GetPlayerBound();
			xBound = bounds.x;
			yBound = bounds.y;
		}

		private void DecreaseGas()
		{
			Fuel--;
			HUDManager.instance.UpdateFuel(Fuel);
			if (Fuel == 20) GameManager.Instance.SpawnFuel();
			if (Fuel == 0) CancelInvoke("DecreaseGas");
		}

		private void Update()
		{
			if (EventSystem.current.currentSelectedGameObject != null &&
				EventSystem.current.currentSelectedGameObject.name == "Rewind")
			{
				Debug.Log("Rewind");
				return;
			}

			if (Input.GetMouseButton(0))
			{
				var pos = MainCamera.WorldToScreenPoint(transform.position);
				var dir = Input.mousePosition - pos;
				var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				if (Fuel > 0) rb.AddRelativeForce(Vector3.right * speed);
			}

			rb.position = new Vector3(Mathf.Clamp(rb.position.x, -xBound, xBound), Mathf.Clamp(rb.position.y, -yBound, yBound), 0);

			if (Input.GetMouseButtonDown(0))
			{
				if (Bullets > 0)
				{
					Instantiate(Bullet, new Vector3(Weapon.position.x, Weapon.position.y, Weapon.position.z), Weapon.rotation);
					Bullets--;
					HUDManager.instance.UpdateBullets(Bullets);
				}
				if (Bullets == 10) GameManager.Instance.SpawnWeapon();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == Tags.Asteroid)
			{
				Destroy(gameObject);
				Destroy(collision.gameObject);
				if (BackgroundController.Instance != null)
					BackgroundController.Instance.Stop();
				if (SoundController.Instance != null)
					SoundController.Instance.PlayAudio(Const.GameSounds.PlayerExplode);
				Time.timeScale = 0;
			}

			if (collision.tag == Tags.Fuel)
			{
				Destroy(collision.gameObject);
				if (SoundController.Instance != null)
					SoundController.Instance.PlayAudio(Const.GameSounds.Collect);
				Fuel = Fuel + 40;
				HUDManager.instance.UpdateFuel(Fuel);
			}

			if (collision.tag == Tags.Weapon)
			{
				Destroy(collision.gameObject);
				if (SoundController.Instance != null)
					SoundController.Instance.PlayAudio(Const.GameSounds.Collect);
				Bullets = Bullets + 30;
				HUDManager.instance.UpdateBullets(Bullets);
			}
		}
	}
}