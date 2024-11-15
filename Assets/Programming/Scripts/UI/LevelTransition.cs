using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
	public static Action OnLevelTransitionAnimDone;
	public static Action OnFirstLevelStartAnimDone;
	
	[SerializeField] private Image _transitionPanel;
	[SerializeField] private TextMeshProUGUI _nextLevelTxt;
	[SerializeField] private TextMeshProUGUI _levelCompleteTxt;
	
	private string _levelTxt = "";
	
	public static LevelTransition Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		LocalizationManager.OnLanguageChanged += LocalizationManager_OnLanguageChanged;
		LocalizationManager_OnLanguageChanged();
	}

	private void LocalizationManager_OnLanguageChanged()
	{
		switch (LocalizationManager.CurrentLanguage)
		{
			case ELanguage.French:
				_levelTxt = "Niveau-";
				break;
			case ELanguage.Arabic:
				_levelTxt = "Level-";
				break;
			case ELanguage.English:
				_levelTxt = "Level-";
				break;
			default:
				break;
		}
	}
	public IEnumerator LevelTransitionAnim()
	{
		LevelManager.Instance.Player.PauseMovement();

		yield return new WaitForSeconds(0.2f);

		_transitionPanel.gameObject.SetActive(true);
		_transitionPanel.color *= new Color(1, 1, 1, 0);
		_transitionPanel.DOColor(_transitionPanel.color + new Color(0, 0, 0, 1), 0.2f);

		_levelCompleteTxt.gameObject.SetActive(true);
		_levelCompleteTxt.color *= new Color(1, 1, 1, 0);
		_levelCompleteTxt.DOColor(_levelCompleteTxt.color + new Color(0, 0, 0, 1), 0f).SetDelay(0.2f);

		LocalizationManager.instance.ChangeLanguage();

		yield return new WaitForSeconds(1.5f);

		_levelCompleteTxt.gameObject.SetActive(false);

		OnLevelTransitionAnimDone?.Invoke();
	}

	public IEnumerator LevelStartAnim(int pLevelID)
	{
		_nextLevelTxt.gameObject.SetActive(true);
		_nextLevelTxt.text = _levelTxt + pLevelID;

		_nextLevelTxt.color *= new Color(1, 1, 1, 0);
		_nextLevelTxt.DOColor(_nextLevelTxt.color + new Color(0, 0, 0, 1), 0.5f);

		yield return new WaitForSeconds(1);

		_nextLevelTxt.DOColor(_nextLevelTxt.color - new Color(0, 0, 0, 1), 0.5f);
		_transitionPanel.DOColor(_transitionPanel.color - new Color(0, 0, 0, 1), 0.3f).SetDelay(0.2f);

		yield return new WaitForSeconds(0.51f);

		_transitionPanel.gameObject.SetActive(false);
		_nextLevelTxt.gameObject.SetActive(false);

		StopAllCoroutines();
        
		InputManager.Instance.UnlockInput();
	}

	public IEnumerator FirstLevelStartAnim(int pLevel)
	{
		_transitionPanel.gameObject.SetActive(true);
		_transitionPanel.color *= new Color(1, 1, 1, 0);
		_transitionPanel.color += new Color(0, 0, 0, 1);

		yield return new WaitForSeconds(0.2f);

		OnFirstLevelStartAnimDone?.Invoke();

		StartCoroutine(LevelStartAnim(pLevel));
	}

	private void OnDestroy()
	{
		LocalizationManager.OnLanguageChanged -= LocalizationManager_OnLanguageChanged;
	}
}
