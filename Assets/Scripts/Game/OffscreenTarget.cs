using UnityEngine;

namespace Assets.Scripts.Game
{
	public class OffscreenTarget : MonoBehaviour
	{
		public Renderer Arrow;
		Vector3 targetPosition;
		Vector2 arrowPosition;
		float max;
		Camera MainCamera;

		private void Start()
		{
			MainCamera = Camera.main;
		}

		private void Update()
		{
			PositionArrow();
		}

		void PositionArrow()
		{
			targetPosition = MainCamera.WorldToViewportPoint(transform.position); //get viewport positions
			if (targetPosition.x >= 0 && targetPosition.x <= 1 && targetPosition.y >= 0 && targetPosition.y <= 1)
			{
				Arrow.enabled = false;
				return;
			}

			var targetPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);
			Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
			float angle = Mathf.Atan2(targetPosOnScreen.y - center.y, targetPosOnScreen.x - center.x) * Mathf.Rad2Deg;

			Debug.DrawLine(MainCamera.transform.position, transform.position);

			arrowPosition = new Vector2(targetPosition.x - 0.5f, targetPosition.y - 0.5f) * 2; //2D version, new mapping
			max = Mathf.Max(Mathf.Abs(arrowPosition.x), Mathf.Abs(arrowPosition.y)); //get largest offset
			arrowPosition = (arrowPosition / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping

			Arrow.transform.position = Camera.main.ViewportToWorldPoint(arrowPosition);
			Arrow.transform.eulerAngles = new Vector3(0, 0, angle);
			Arrow.enabled = true;
		}
	}
}
