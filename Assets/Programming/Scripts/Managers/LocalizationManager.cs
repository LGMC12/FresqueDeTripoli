using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELanguage
{
	French, Arabic, English
}

public class LocalizationManager : MonoBehaviour
{
	public static Action OnLanguageChanged;

	private static ELanguage _currentLanguage = ELanguage.French;

	public static ELanguage CurrentLanguage
    {
		get => _currentLanguage;
		set
        {
			_currentLanguage = value;
			OnLanguageChanged?.Invoke();
		}
    }
}