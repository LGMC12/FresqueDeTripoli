using System;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
using TMPro;
using UnityEngine;

public class ArabicLanguageFixer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txt;

    [ContextMenu("Fix")]
    private void Update()
    {
        _txt.text = ArabicFixer.Fix(_txt.text);
    }
}
