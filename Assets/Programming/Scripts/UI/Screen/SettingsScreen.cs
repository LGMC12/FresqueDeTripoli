using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class SettingsScreen : ScreenUI
{
    public static Action OnBack;

    [SerializeField] private AnimatedButton _back;

    protected override void Start()
    {
        _back.OnPlay += Back;
    }

    private void Back() { OnBack?.Invoke(); }

    protected override void OnDestroy()
    {
        _back.OnPlay -= Back;
    }
}