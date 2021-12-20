using UnityEngine;

namespace Assets.Scripts.Game
{
	public class BackgroundController : MonoBehaviour
	{
		private static BackgroundController _instance;
		public static BackgroundController Instance { get { return _instance; } }

		private AudioSource AudioSource;
		public bool IsPaused;
		public bool StopAtAll;

		void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
				AudioSource = GetComponent<AudioSource>();
			}
		}

		public void Pause()
		{
			IsPaused = true;
			AudioSource.Pause();
		}

		public void Play()
		{
			if (StopAtAll || AudioSource.isPlaying) return;

			if (IsPaused)
				AudioSource.UnPause();
			else
				AudioSource.Play();

			IsPaused = false;
		}

		public void Stop()
		{
			AudioSource.Stop();
			StopAtAll = true;
		}

		public void UnStop()
		{
			StopAtAll = false;
		}

		public void UnStopAndPlay()
		{
			StopAtAll = false;
			Play();
		}
	}
}
