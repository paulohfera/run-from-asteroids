using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game
{
	public class AsteroidController : MonoBehaviour
	{
		public float speed = 1f;
		public int score = 10;

		private void Update()
		{
			transform.Translate(new Vector2(speed * Time.deltaTime, 0));

			if (Input.GetMouseButton(0) && EventSystem.current.currentSelectedGameObject != null)
			{
				Debug.Log("Name: " + EventSystem.current.currentSelectedGameObject.name);
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Debug.Log(collision.name);
			if (collision.tag == Tags.Bullet)
			{
				if (SoundController.Instance != null)
					SoundController.Instance.PlayAudio(Const.GameSounds.AsteroidDestroy);
				Destroy(gameObject);
				Destroy(collision.gameObject);
				HUDManager.instance.UpdateScore(score);
			}
		}
	}
}