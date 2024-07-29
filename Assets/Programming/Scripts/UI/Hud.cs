using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
	public static Action OnTransitionDone;
	public static Action OnTutoDone;

	[SerializeField] private TextMeshProUGUI _seedTxt;
	[SerializeField] private TextMeshProUGUI _cheeseTxt;
	[SerializeField] private TextMeshProUGUI _addsTxt;

	[SerializeField] private TextMeshProUGUI _seedSavedTxt;
	[SerializeField] private TextMeshProUGUI _cheeseSavedTxt;
	[SerializeField] private TextMeshProUGUI _addsSavedTxt;

	[SerializeField] private GameObject _seeds;
	[SerializeField] private GameObject _cheeses;
	[SerializeField] private GameObject _adds;

	[Space(20)]
	[SerializeField] private Image _transitionPanel;
	[SerializeField] private TextMeshProUGUI _nextLevelTxt;
	[SerializeField] private TextMeshProUGUI _levelCompleteTxt;

	[SerializeField] private ShopUI _shopUI;

	[SerializeField] private TextMeshProUGUI _endTxt;
	[SerializeField] private Image _endPanel;

	private string _levelTxt = "";

	public ShopUI ShopUI { get => _shopUI; }

	public static Hud Instance { get; private set; }

	private int _maxSeeds;
	private int _maxCheese;
	private int _maxAdds;

	private int _seedsSaved;
	private int _cheeseSaved;
	private int _addsSaved;

	private void Awake()
	{
		Instance = this;
		LocalizationManager.OnLanguageChanged += LocalizationManager_OnLanguageChanged;
    }

    private void LocalizationManager_OnLanguageChanged()
    {
		switch (LocalizationManager.CurrentLanguage)
		{
			case ELanguage.French:
				_levelTxt = "Niveau ";
				break;
			case ELanguage.Arabic:
                _levelTxt = "Level ";
                break;
			case ELanguage.English:
                _levelTxt = "Level ";
                break;
			default:
				break;
		}
	}

    public void Start()
	{
		_cheeses.SetActive(false);
		_adds.SetActive(false);

		LocalizationManager_OnLanguageChanged();

		_seeds.transform.localScale = Vector2.one;
		_cheeses.transform.localScale = Vector2.one;
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

	public void OnSaved(int pSeeds, int pCheeses, int pAdds)
	{
		_seedSavedTxt.text = "(" + (_maxSeeds - pSeeds) + ")";
		_cheeseSavedTxt.text = "(" + (_maxCheese - pCheeses) + ")";
		_addsSavedTxt.text = "(" + (_maxAdds - pAdds) + ")";

		_seedSavedTxt.rectTransform.DOScale(1.25f, 0.15f).SetDelay(0.05f).OnComplete(() =>
		{
			_seedSavedTxt.rectTransform.DOScale(1f, 0.1f);
		});

		_cheeseSavedTxt.rectTransform.DOScale(1.25f, 0.15f).SetDelay(0.1f).OnComplete(() =>
		{
			_cheeseSavedTxt.rectTransform.DOScale(1f, 0.1f);
		});

		_addsSavedTxt.rectTransform.DOScale(1.25f, 0.15f).SetDelay(0.15f).OnComplete(() =>
		{
			_addsSavedTxt.rectTransform.DOScale(1f, 0.1f);
		});
	}

	public void ShowCheeses()
	{
		_cheeses.SetActive(true);
		_seeds.transform.localScale = Vector2.one * 0.7f;
	}

	public void ShowAdds()
	{
		_adds.SetActive(true);
		_cheeses.transform.localScale = Vector2.one * 0.7f;
	}

	public IEnumerator LevelTransition()
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

		OnTransitionDone?.Invoke();
	}

	public IEnumerator LevelStart(int pLevelID)
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
    }

	public IEnumerator TutoStart()
    {
        _transitionPanel.gameObject.SetActive(true);
        _transitionPanel.color *= new Color(1, 1, 1, 0);
		_transitionPanel.color += new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.2f);

		OnTutoDone?.Invoke();

        StartCoroutine(LevelStart(0));
	}

	public IEnumerator EndGame()
	{
        _endPanel.gameObject.SetActive(true);
        _endPanel.color *= new Color(1, 1, 1, 0);
        _endPanel.DOColor(_endPanel.color + new Color(0, 0, 0, 1), 0.2f);

        _transitionPanel.DOColor(_transitionPanel.color - new Color(0, 0, 0, 1), 0f).SetDelay(0.2f);

        _endTxt.gameObject.SetActive(true);
        _endTxt.color *= new Color(1, 1, 1, 0);
        _endTxt.DOColor(_endTxt.color + new Color(0, 0, 0, 1), 0f).SetDelay(0.2f);

		yield return new WaitForSeconds(1.5f);

        AsyncOperation lOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

		while (!lOperation.isDone)
		{
            yield return null;
        }

		Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChanged -= LocalizationManager_OnLanguageChanged;
    }
}