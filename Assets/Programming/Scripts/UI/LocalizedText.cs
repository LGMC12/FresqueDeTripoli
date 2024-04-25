using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArabicSupport;
using TMPro;


public class LocalizedText : MonoBehaviour
{
	[SerializeField] private List<string> _text = new List<string>();
	[SerializeField] private TextMeshProUGUI _textMeshPro;

	private Dictionary<ELanguage, string> _localizedItem;

    private void Awake()
    {
		_localizedItem = new Dictionary<ELanguage, string>()
		{
			{ELanguage.French, _text[0]},
			{ELanguage.Arabic, _text[1]},
			{ELanguage.English, _text[2]}
		};

		LocalizationManager.OnLanguageChanged += LocalizationManager_OnLanguageChanged;
    }

    private void LocalizationManager_OnLanguageChanged()
	{
		_textMeshPro.text = ArabicFixer.Fix(_localizedItem[LocalizationManager.CurrentLanguage]);
	}

    void Start()
	{
		LocalizationManager_OnLanguageChanged();
	}

    private void OnDestroy()
	{
		LocalizationManager.OnLanguageChanged -= LocalizationManager_OnLanguageChanged;
	}
}