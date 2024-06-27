using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum ELanguage
{
	French, Arabic, English
}

public class LocalizationManager : MonoBehaviour
{
	public static Action OnLanguageChanged;

	private static ELanguage _currentLanguage = ELanguage.Arabic;

	public static ELanguage CurrentLanguage
    {
		get => _currentLanguage;
		set
        {
			_currentLanguage = value;
			OnLanguageChanged?.Invoke();
		}
    }

	[SerializeField] public TMP_FontAsset _arabicFont;
	[SerializeField] public TMP_FontAsset _latinFont;

	private static LocalizationManager _instance;
	public static LocalizationManager instance
	{
		get => _instance;
	}

    private void Awake()
    {
        _instance = this;
    }
}