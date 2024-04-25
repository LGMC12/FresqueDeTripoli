using System;
using TMFunds.UI;
using UnityEngine;

namespace Com.IsartDigital.Chaource
{
	public class MainScreen : ScreenUI
	{
		public static Action OnPlay;
		public static Action OnExplore;
		public static Action OnAbout;
		public static Action OnSettings;

		[SerializeField] private AnimatedButton _play;
		[SerializeField] private AnimatedButton _explore;
		[SerializeField] private AnimatedButton _about;
		[SerializeField] private AnimatedButton _settings;

		// Start is called before the first frame update
		protected override void Start()
		{
			base.Start();

			_play.OnPlay += StartGame;
			_explore.OnPlay += Explore;
			_about.OnPlay += About;
			_settings.OnPlay += Settings;
		}

		private void StartGame() { OnPlay?.Invoke(); }

		private void Explore() { OnExplore?.Invoke(); }

		private void About() { OnAbout?.Invoke(); }

		private void Settings() { OnSettings?.Invoke(); }


		protected override void OnDestroy()
		{
			base.OnDestroy();

			_play.OnPlay -= StartGame;
			_explore.OnPlay -= Explore;
			_about.OnPlay -= About;
			_settings.OnPlay -= Settings;
		}
	}
}