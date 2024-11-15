using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _seedTxt;
	[SerializeField] private TextMeshProUGUI _cheeseTxt;
	[SerializeField] private TextMeshProUGUI _addsTxt;

	[SerializeField] private GameObject _seeds;
	[SerializeField] private GameObject _cheeses;
	[SerializeField] private GameObject _adds;

	[SerializeField] private Button _backButton;

	public static Hud Instance { get; private set; }

	private int _maxSeeds;
	private int _maxCheese;
	private int _maxAdds;

	private void Awake()
	{
		Instance = this;
		_backButton.onClick.AddListener(Back);
    }

	public void SaveAnim()
	{
		switch (LevelManager.Level.Phase)
		{
			case EGamePhase.Seeds:
				_seedTxt.DOColor(Color.green, 0.2f);
				_seedTxt.rectTransform.DOScale(Vector3.one * 1.35f, 0.2f);
				_seedTxt.rectTransform.DOScale(Vector3.one, 0.2f).SetDelay(0.25f);
				_seedTxt.DOColor(Color.black, 0.2f).SetDelay(0.25f);
				return;
				
			case EGamePhase.Cheeses:
				_cheeseTxt.DOColor(Color.green, 0.2f);
				_cheeseTxt.rectTransform.DOScale(Vector3.one * 1.35f, 0.2f);
				_cheeseTxt.rectTransform.DOScale(Vector3.one, 0.2f).SetDelay(0.25f);
				_cheeseTxt.DOColor(Color.black, 0.2f).SetDelay(0.25f);
				return;
				
			case EGamePhase.Adds :
				_addsTxt.DOColor(Color.green, 0.2f);
				_addsTxt.rectTransform.DOScale(Vector3.one * 1.35f, 0.2f);
				_addsTxt.rectTransform.DOScale(Vector3.one, 0.2f).SetDelay(0.25f);
				_addsTxt.DOColor(Color.black, 0.2f).SetDelay(0.25f);
				return;
				
			default: return;
		}
		
	}

	public void UpdateTxt(int pSeeds, int pCheeses, int pAdds)
	{
		_seedTxt.text = "" + (_maxSeeds - pSeeds) + " / " + _maxSeeds;
		_cheeseTxt.text = "" + (_maxCheese - pCheeses) + " / " + _maxCheese;
		_addsTxt.text = "" + (_maxAdds - pAdds) + " / " + _maxAdds;
	}

	public void SetMax(int pSeeds, int pCheeses, int pAdds)
	{
		_maxSeeds = pSeeds;
		_maxCheese = pCheeses;
		_maxAdds = pAdds;
	}

	private void Back()
	{
		LoadingScreen.Instance.ShowLoadingScreen();
		StartCoroutine(BackToMainMenu());
	}

	private IEnumerator BackToMainMenu()
	{
		AsyncOperation lOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

		while (!lOperation.isDone)
		{
			yield return null;
		}
	}

	private void OnDestroy()
	{
		_backButton.onClick.RemoveAllListeners();
	}
}