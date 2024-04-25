using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMFunds.UI;
public class CompositionSelector : ScreenUI
{
	public static Action OnBack;
	public static Action<int> OnCompositionSelected;

	[SerializeField] private AnimatedButton _back;

	protected override void Start()
	{
		_back.OnPlay += Back;

		CompositionButton.OnClick += SelectComposition;
	}

	private void Back()
	{
		OnBack?.Invoke();
	}

	private void SelectComposition(int pIndex)
	{
		OnCompositionSelected?.Invoke(pIndex);
	}

	protected override void OnDestroy()
	{
		_back.OnPlay -= Back;

		CompositionButton.OnClick -= SelectComposition;
	}
}