using System;
using TMFunds.UI;
using UnityEngine;

namespace Com.IsartDigital.Chaource
{
	public class MainScreen : ScreenUI
	{
		public static Action OnPlay;
		public static Action OnFresco;

		[SerializeField] private string _instaUrl;

		[SerializeField] private LocalizedURL _aboutUrl;

		[SerializeField] private AnimatedButton _play;
		[SerializeField] private AnimatedButton _fresco;
		[SerializeField] private AnimatedButton _about;
		[SerializeField] private AnimatedButton _insta;


		protected override void Start()
		{
			base.Start();

			_play.OnPlay += StartGame;
			_fresco.OnPlay += Fresco;
			_about.OnPlay += About;
			_insta.OnPlay += Insta;
		}

		private void StartGame() { OnPlay?.Invoke(); }
		private void Fresco() { OnFresco?.Invoke(); }
		private void About() { Application.OpenURL(_aboutUrl.URL); }
		private void Insta() { Application.OpenURL(_instaUrl); }


		protected override void OnDestroy()
		{
			base.OnDestroy();

            _play.OnPlay -= StartGame;
            _fresco.OnPlay -= Fresco;
            _about.OnPlay -= About;
            _insta.OnPlay -= Insta;
        }
	}
}