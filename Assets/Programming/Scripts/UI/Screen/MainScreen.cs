using System;
using TMFunds.UI;
using UnityEngine;

namespace Com.IsartDigital.Chaource
{
	public class MainScreen : ScreenUI
	{
		public static Action OnPlay;
		public static Action OnSettings;

		[SerializeField] private AnimatedButton _play;
		[SerializeField] private AnimatedButton _settings;

		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();

			_play.OnPlay += StartGame;
			_settings.OnPlay += Settings;
		}

		private void StartGame() { OnPlay?.Invoke(); }

		private void Settings() { OnSettings?.Invoke(); }


		protected override void OnDestroy()
		{
			base.OnDestroy();

			_play.OnPlay -= StartGame;
			_settings.OnPlay -= Settings;
		}
	}
}