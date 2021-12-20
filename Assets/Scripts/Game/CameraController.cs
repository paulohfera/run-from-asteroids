using UnityEngine;

namespace Assets.Scripts.Game
{
	public class CameraController : MonoBehaviour
	{
		public GameObject player;

		private float yMax = 5f;
		private float yMin = -5f;
		private float xMax = 10f;
		private float xMin = -10f;

		void Update()
		{
			if (player == null) return;

			//if within the bounds, camera locks onto player
			if (player.transform.position.y < yMax && player.transform.position.y > yMin)
			{
				transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
			}

			//if player is above/below the y axis binders, camera locks to player on xAxis and stays stationary 
			//on yAxis
			if (player.transform.position.y > yMax)
			{
				transform.position = new Vector3(player.transform.position.x, yMax, -1f);
			}
			else if (player.transform.position.y < yMin)
			{
				transform.position = new Vector3(player.transform.position.x, yMin, -1f);
			}

			//if player is right/left of the xAxis binders, camera locks to player on yAxis and stays stationary 
			//on xAxis
			if (player.transform.position.x > xMax)
			{
				transform.position = new Vector3(xMax, player.transform.position.y, -1f);
			}
			else if (player.transform.position.x < xMin)
			{
				transform.position = new Vector3(xMin, player.transform.position.y, -1f);
			}

			//if player is above the yAxis binder, and to the right of the xAxis, the camera stays stationary
			if (player.transform.position.y > yMax && player.transform.position.x > xMax)
			{
				transform.position = new Vector3(xMax, yMax, -1f);
			}
			//if player is above the yAxis binder, and to the left of the xAxis, the camera stays stationary
			if (player.transform.position.y > yMax && player.transform.position.x < xMin)
			{
				transform.position = new Vector3(xMin, yMax, -1f);
			}
			//if player is below the yAxis binder, and to the right of the xAxis, the camera stays stationary
			if (player.transform.position.y < yMin && player.transform.position.x > xMax)
			{
				transform.position = new Vector3(xMax, yMin, -1f);
			}
			//if player is below the yAxis binder, and to the left of the xAxis, the camera stays stationary
			if (player.transform.position.y < yMin && player.transform.position.x < xMin)
			{
				transform.position = new Vector3(xMin, yMin, -1f);
			}
		}
	}
}