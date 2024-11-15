using System;
using System.Collections;
using System.Collections.Generic;
using Com.IsartDigital.Chaource;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;
	public static UIManager Instance { get => _instance; private set { _instance = value; } }

	public static Dictionary<ScreenChannel, ScreenUI> ScreenList = new Dictionary<ScreenChannel, ScreenUI>();
	[SerializeField] private List<ScreenChannel> ScreenChannelList = new List<ScreenChannel>();
	[SerializeField] private List<ScreenUI> ScreenUIList = new List<ScreenUI>();

	[SerializeField] private AudioSource _uiLoop;

	private static ScreenUI _currentScreen;
	public static ScreenUI CurrentScreen { get => _currentScreen; private set { _currentScreen = value; } }

	private static bool _hasSplashDone = false;
	
	public static bool tutoFinished = false;

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

		MainScreen.OnPlay += OpenSelector;
		MainScreen.OnFresco += OpenFresco;

		FrescoScreen.OnBack += CloseFresco;
		FrescoScreen.OnCompo += OpenCompo;

		CompositionScreen.OnBack += CloseComposition;

		LevelSelector.OnBack += CloseSelector;
		LevelSelector.OnLevelSelected += StartGame;

		TutoScreen.OnTutoFinished += StartTutoLevel;
	}

	private void StartTutoLevel()
	{
		StartGame(0);
	}

	private void StartGame(int pLevel)
	{
		LoadingScreen.Instance.ShowLoadingScreen();
		StartCoroutine(StartGameLevel(pLevel));
	}

	private void Start()
    {
		if (_hasSplashDone)
        {
	        UIScreen.Close(ScreenChannel.SPLASH);
            SetCurrentScreen(ScreenChannel.MAIN0);
            UIScreen.Open(ScreenChannel.MAIN0);
            _uiLoop.Play();
        }
		else
        {
            SetCurrentScreen(ScreenChannel.SPLASH);
            UIScreen.Open(ScreenChannel.SPLASH);
        }
    }

	private void OpenSelector()
	{
		if (tutoFinished)
		{
			UIScreen.Open(ScreenChannel.LEVELSELECTOR);
			UIScreen.Close(ScreenChannel.MAIN0);
		}
		else
		{
			UIScreen.Open(ScreenChannel.GAME0);	
			UIScreen.Close(ScreenChannel.MAIN0);
		}
	}
	
	private void CloseSelector()
	{
		UIScreen.Close(ScreenChannel.LEVELSELECTOR);
		UIScreen.Open(ScreenChannel.MAIN0);
	}

    private void CloseComposition()
    {
        UIScreen.Close(ScreenChannel.MAIN2);
        UIScreen.Open(ScreenChannel.MAIN1);
    }

    private void OpenCompo()
    {
		UIScreen.Close(ScreenChannel.MAIN1);
        UIScreen.Open(ScreenChannel.MAIN2);
    }

    private void CloseFresco()
    {
		UIScreen.Close(ScreenChannel.MAIN1);
		UIScreen.Open(ScreenChannel.MAIN0);
    }

    private void OpenFresco()
    {
		UIScreen.Close(ScreenChannel.MAIN0);
		UIScreen.Open(ScreenChannel.MAIN1);
    }

	private IEnumerator StartGameLevel(int pLevelIndex)
	{
		LevelManager.StartIndex = pLevelIndex;
		
		UIScreen.Close(ScreenChannel.LEVELSELECTOR);
		
		AsyncOperation lOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
		
		while (!lOperation.isDone)
		{
			yield return null;
		}
	}

	private void SelectedLanguage()
	{
		UIScreen.Close(ScreenChannel.OTHER0);
		UIScreen.Open(ScreenChannel.MAIN0);
		_uiLoop.Play();
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

		MainScreen.OnPlay -= OpenSelector;
        MainScreen.OnFresco -= OpenFresco;

        FrescoScreen.OnBack -= CloseFresco;
        FrescoScreen.OnCompo -= OpenCompo;

        CompositionScreen.OnBack -= CloseComposition;
        
        LevelSelector.OnBack -= CloseSelector;
        LevelSelector.OnLevelSelected -= StartGame;
        
        TutoScreen.OnTutoFinished -= StartTutoLevel;
    }
}