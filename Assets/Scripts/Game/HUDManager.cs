using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
	public class HUDManager : MonoBehaviour
	{
		private static HUDManager _instance;
		public static HUDManager instance { get { return _instance; } }

		private new Camera camera;
		private Vector3 View;
		private float WhidthBorder = 9f;
		private float HeightBorder = 4f;
		private int TotalScore = 0;

		public Text ScoreText;
		public Text BulletsText;
		public Text FuelText;

		public GameObject Dynamic;

		[HideInInspector]
		public Boundaries Boundaries;

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
			}

			Initialize();
		}

		private void Initialize()
		{
			if (camera == null)
				camera = Camera.main;

			View = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

			CreateBoundaries();
		}

		private void CreateBoundaries()
		{
			var BoundTop = new GameObject("BoundTop");
			BoundTop.AddComponent<BoxCollider2D>().isTrigger = true;
			BoundTop.transform.SetParent(Dynamic.transform);
			BoundTop.tag = "Bound";

			var BoundRight = new GameObject("BoundRight");
			BoundRight.AddComponent<BoxCollider2D>().isTrigger = true;
			BoundRight.transform.SetParent(Dynamic.transform);
			BoundRight.tag = "Bound";

			var BoundBottom = new GameObject("BoundBottom");
			BoundBottom.AddComponent<BoxCollider2D>().isTrigger = true;
			BoundBottom.transform.SetParent(Dynamic.transform);
			BoundBottom.tag = "Bound";

			var BoundLeft = new GameObject("BoundLeft");
			BoundLeft.AddComponent<BoxCollider2D>().isTrigger = true;
			BoundLeft.transform.SetParent(Dynamic.transform);
			BoundLeft.tag = "Bound";

			BoundTop.transform.position = new Vector3(0, View.y + WhidthBorder + 2, 0);
			BoundRight.transform.position = new Vector3(View.x + WhidthBorder + 2, 0, 0);
			BoundBottom.transform.position = new Vector3(0, -(View.y + WhidthBorder + 2), 0);
			BoundLeft.transform.position = new Vector3(-(View.x + WhidthBorder + 2), 0, 0);

			BoundTop.transform.localScale = new Vector3(BoundRight.transform.position.x * 2, 1, 1);
			BoundRight.transform.localScale = new Vector3(1, BoundTop.transform.position.y * 2, 1);
			BoundBottom.transform.localScale = new Vector3(BoundRight.transform.position.x * 2, 1, 1);
			BoundLeft.transform.localScale = new Vector3(1, BoundTop.transform.position.y * 2, 1);
		}

		public Vector3 GetPlayerBound()
		{
			return new Vector3(View.x + WhidthBorder, View.y + HeightBorder);
		}

		public void UpdateFuel(int fuel)
		{
			FuelText.text = fuel.ToString("00");
		}

		public void UpdateBullets(int bullets)
		{
			BulletsText.text = bullets.ToString("00");
		}

		public void UpdateScore(int score)
		{
			TotalScore += score;
			ScoreText.text = string.Format("Score: {0}", TotalScore.ToString("00000"));
		}
	}
}