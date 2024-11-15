using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedURL : MonoBehaviour
{
    [SerializeField] private List<string> _urls = new List<string>();
    
    private Dictionary<ELanguage, string> _localizedItem;
    public string URL { get; private set; }
    
    private void Awake()
    {
        _localizedItem = new Dictionary<ELanguage, string>()
        {
            {ELanguage.French, _urls[0]},
            {ELanguage.Arabic, _urls[1]},
            {ELanguage.English, _urls[2]}
        };

        LocalizationManager.OnLanguageChanged += LocalizationManager_OnLanguageChanged;
    }

    private void LocalizationManager_OnLanguageChanged()
    {
        URL = _localizedItem[LocalizationManager.CurrentLanguage];
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
