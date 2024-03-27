using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.UI;

public class LanguageScreen : ScreenUI
{
    public static Action OnLanguageSelected;

	[SerializeField] private UIButton _french;
	[SerializeField] private UIButton _arabic;
	[SerializeField] private UIButton _english;

    protected override void Start()
    {
        base.Start();

        _french.OnPlay += French;
        _arabic.OnPlay += Arabic;
        _english.OnPlay += English;
    }

    private void English()
    {
        LocalizationManager.CurrentLanguage = ELanguage.English;
        OnLanguageSelected?.Invoke();
    }

    private void Arabic()
    {
        LocalizationManager.CurrentLanguage = ELanguage.Arabic;
        OnLanguageSelected?.Invoke();
    }

    private void French()
    {
        LocalizationManager.CurrentLanguage = ELanguage.French;
        OnLanguageSelected?.Invoke();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _french.OnPlay -= French;
        _arabic.OnPlay -= Arabic;
        _english.OnPlay -= English;
    }
}
