using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class AboutScreen : ScreenUI
{
    public static Action OnBack;

    [SerializeField] private AnimatedButton _back;

    override protected void Start()
    {
        _back.OnPlay += Back;
    }

    private void Back()
    {
        OnBack?.Invoke();
    }
}