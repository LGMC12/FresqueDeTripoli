using Com.IsartDigital.Chaource;
using System;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject _compo;

	private static UIManager _instance;
	public static UIManager Instance { get => _instance; private set { _instance = value; } }

	public static Dictionary<ScreenChannel, ScreenUI> ScreenList = new Dictionary<ScreenChannel, ScreenUI>();
	[SerializeField] private List<ScreenChannel> ScreenChannelList = new List<ScreenChannel>();
	[SerializeField] private List<ScreenUI> ScreenUIList = new List<ScreenUI>();

	private static ScreenUI _currentScreen;
	public static ScreenUI CurrentScreen { get => _currentScreen; private set { _currentScreen = value; } }

	void Start()
	{
		for (int i = 0; i < ScreenChannelList.Count; i++)
		{
			ScreenList.Add(ScreenChannelList[i], ScreenUIList[i]);
		}

		SetCurrentScreen(ScreenChannel.SPLASH);
		UIScreen.Open(ScreenChannel.SPLASH);

		SplashScreenAnimationEvent.OnClosed += Splash_Close;

		LanguageScreen.OnLanguageSelected += SelectedLanguage;

		MainScreen.OnPlay += StartGame;
		MainScreen.OnExplore += StartExplore;
		MainScreen.OnAbout += Open_About;
		MainScreen.OnSettings += OpenSettings;

		SettingsScreen.OnBack += CloseSettings;

		AboutScreen.OnBack += CloseAbout;

		CompositionSelector.OnBack += CloseCompositionSelector;
		CompositionSelector.OnCompositionSelected += OpenCompostionHud;

		CompositionHud.OnBack += CloseCompositionHud;
	}

    private void OpenCompostionHud(int obj)
    {
		UIScreen.Close(ScreenChannel.MAIN1);
		UIScreen.Open(ScreenChannel.HUD1);
    }

    private void CloseCompositionHud()
    {
		UIScreen.Close(ScreenChannel.HUD1);
		UIScreen.Open(ScreenChannel.MAIN1);
    }

    private void CloseCompositionSelector()
    {
		UIScreen.Close(ScreenChannel.MAIN1);
		UIScreen.Open(ScreenChannel.MAIN0);
    }

    private void CloseAbout()
    {
		UIScreen.Close(ScreenChannel.OTHER1);
    }

    private void CloseSettings()
    {
		UIScreen.Close(ScreenChannel.OPTION0);
    }

    private void OpenSettings()
    {
		UIScreen.Open(ScreenChannel.OPTION0);
    }

    private void Open_About()
	{
		UIScreen.Open(ScreenChannel.OTHER1);
	}

	private void StartExplore()
	{
		UIScreen.Close(ScreenChannel.MAIN0);
		UIScreen.Open(ScreenChannel.MAIN1);
	}

	private void StartGame()
	{
		SceneManager.LoadScene(1, LoadSceneMode.Single);
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
		MainScreen.OnExplore -= StartExplore;
		MainScreen.OnAbout -= Open_About;
		MainScreen.OnSettings -= OpenSettings;

		SettingsScreen.OnBack -= CloseSettings;

		AboutScreen.OnBack -= CloseAbout;

		CompositionSelector.OnBack -= CloseCompositionSelector;
		CompositionSelector.OnCompositionSelected -= OpenCompostionHud;

		CompositionHud.OnBack -= CloseCompositionHud;
	}
}