using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class CompositionButton : AnimatedButton
{
    public static Action<int> OnClick;

	[SerializeField] private int _compoIndex;

	public int CompositionIndex
    {
        get => _compoIndex;
        set
        {
            _compoIndex = value;
        }
    }

    protected override void Clicking()
    {
        OnClick?.Invoke(_compoIndex);
    }
}