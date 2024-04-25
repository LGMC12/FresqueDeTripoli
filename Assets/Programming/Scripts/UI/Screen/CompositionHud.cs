using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class CompositionHud : ScreenUI
{
    public static Action OnBack;

	[SerializeField] private AnimatedButton _back;

    protected override void Start()
    {
        _back.OnPlay += Back;
    }

    private void Back()
    {
        OnBack?.Invoke();
        CompositionManager.HideAll();
    }

    protected override void OnDestroy()
    {
        _back.OnPlay -= Back;
    }
}