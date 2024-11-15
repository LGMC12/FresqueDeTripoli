using System;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
	[SerializeField] private TMP_Dropdown _languages;

	void Start()
	{
		LocalizationManager.OnLanguageChanged += ChangeLanguageValue;
		_languages.options[1].text = ArabicFixer.Fix(_languages.options[1].text);
	}

    private void ChangeLanguageValue()
	{
		_languages.SetValueWithoutNotify((int)LocalizationManager.CurrentLanguage);
	}

    public void SelectLanguage(int pLanguageIndex)
	{
		LocalizationManager.CurrentLanguage = (ELanguage)pLanguageIndex;
	}

    private void OnDestroy()
    {
		LocalizationManager.OnLanguageChanged -= ChangeLanguageValue;
    }
}