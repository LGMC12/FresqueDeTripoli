using Com.IsartDigital.Chaource;
using System;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	//[SerializeField] private GameObject _compo;

	private static UIManager _instance;
	public static UIManager Instance { get => _instance; private set { _instance = value; } }

	public static Dictionary<ScreenChannel, ScreenUI> ScreenList = new Dictionary<ScreenChannel, ScreenUI>();
	[SerializeField] private List<ScreenChannel> ScreenChannelList = new List<ScreenChannel>();
	[SerializeField] private List<ScreenUI> ScreenUIList = new List<ScreenUI>();

	private static ScreenUI _currentScreen;
	public static ScreenUI CurrentScreen { get => _currentScreen; private set { _currentScreen = value; } }

	private static bool _hasSplashDone = false;

	void Awake()
	{
		if (Instance != null) { Destroy(gameObject); }

		_instance = this;

        ScreenList.Clear();

        for (int i = 0; i < ScreenChannelList.Count; i++)
		{
			ScreenList.Add(ScreenChannelList[i], ScreenUIList[i]);
		}

		SplashScreenAnimationEvent.OnClosed += Splash_Close;

		LanguageScreen.OnLanguageSelected += SelectedLanguage;

		MainScreen.OnPlay += StartGame;
		MainScreen.OnSettings += OpenSettings;

		SettingsScreen.OnBack += CloseSettings;
	}

    private void Start()
    {
		if (_hasSplashDone)
        {
            SetCurrentScreen(ScreenChannel.MAIN0);
            UIScreen.Open(ScreenChannel.MAIN0);
			print("main menu");
        }
		else
        {
            SetCurrentScreen(ScreenChannel.SPLASH);
            UIScreen.Open(ScreenChannel.SPLASH);
			print("splash");
        }
    }

    private void CloseSettings()
    {
		UIScreen.Close(ScreenChannel.OPTION0);
    }

    private void OpenSettings()
    {
		UIScreen.Open(ScreenChannel.OPTION0);
    }

	private void StartGame()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
		UIScreen.Close(ScreenChannel.MAIN0);
	}

	private void SelectedLanguage()
	{
		UIScreen.Close(ScreenChannel.OTHER0);
		UIScreen.Open(ScreenChannel.MAIN0);
	}

	private void Splash_Close()
	{
		UIScreen.Close(ScreenChannel.SPLASH);
		UIScreen.Open(ScreenChannel.OTHER0);
		_hasSplashDone = true;
	}

	public static void SetCurrentScreen(ScreenChannel pScreen)
	{
		if (ScreenList.ContainsKey(pScreen))
		{
			CurrentScreen = ScreenList[pScreen];
		}
	}

	private void OnDestroy()
	{
		SplashScreenAnimationEvent.OnClosed -= Splash_Close;

		LanguageScreen.OnLanguageSelected -= SelectedLanguage;

		MainScreen.OnPlay -= StartGame;
		MainScreen.OnSettings -= OpenSettings;

		SettingsScreen.OnBack -= CloseSettings;
	}
}