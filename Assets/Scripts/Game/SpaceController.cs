using UnityEngine;

namespace Assets.Scripts.Game
{
	public class SpaceController : MonoBehaviour
	{
		private static SpaceController _instance;
		public static SpaceController Instance { get { return _instance; } }

		public float parralax = 4f;

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
		}

		private void Update()
		{
			Material mat = GetComponent<MeshRenderer>().material;
			Vector2 offset = mat.mainTextureOffset;

			offset.x = transform.position.x / transform.localScale.x / parralax;
			offset.y = transform.position.y / transform.localScale.y / parralax;

			mat.mainTextureOffset = offset;
		}
	}
}
