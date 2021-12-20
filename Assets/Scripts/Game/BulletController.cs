using UnityEngine;

namespace Assets.Scripts.Game
{

	public class BulletController : MonoBehaviour
	{
		public float speed = 1.5f;

		void Update()
		{
			transform.Translate(new Vector2(speed * Time.deltaTime, 0));
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.tag == "Bound")
			{
				Destroy(gameObject);
			}
		}
	}
}