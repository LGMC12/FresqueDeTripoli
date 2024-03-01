using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.UI;

public enum ELangauge
{
	French, 
	Arabic, 
	English
}

public class LanguageScreen : ScreenUI
{
    public static Action OnLanguageSelected;

	[SerializeField] private UIButton _french;
	[SerializeField] private UIButton _arabic;
	[SerializeField] private UIButton _english;

    protected override void Start()
    {
        base.Start();

        _french.OnPlay += SetLanguage;
        _arabic.OnPlay += SetLanguage;
        _english.OnPlay += SetLanguage;
    }

    private void SetLanguage()
    {
        OnLanguageSelected?.Invoke();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _french.OnPlay -= SetLanguage;
        _arabic.OnPlay -= SetLanguage;
        _english.OnPlay -= SetLanguage;
    }
}
