using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _endTxt;
    [SerializeField] private Image _endPanel;

    [SerializeField] private Image _transitionPanel;
    
    public static EndGame Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator EndGameAnim()
    {
        Hud.Instance.gameObject.SetActive(false);
        
        _endPanel.gameObject.SetActive(true);
        _endPanel.color *= new Color(1, 1, 1, 0);
        _endPanel.DOColor(_endPanel.color + new Color(0, 0, 0, 1), 0.2f);

        _transitionPanel.DOColor(_transitionPanel.color - new Color(0, 0, 0, 1), 0f).SetDelay(0.2f);

        _endTxt.gameObject.SetActive(true);
        _endTxt.color *= new Color(1, 1, 1, 0);
        _endTxt.DOColor(_endTxt.color + new Color(0, 0, 0, 1), 0f).SetDelay(0.2f);
        
        yield return new WaitForSeconds(1);
        
        LoadingScreen.Instance.ShowLoadingScreen();
        StartCoroutine(BackToMainMenu());
        
        yield return null;
    }

    private IEnumerator BackToMainMenu()
    {
        AsyncOperation lOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

        while (!lOperation.isDone)
        {
            yield return null;
        }
    }
}
