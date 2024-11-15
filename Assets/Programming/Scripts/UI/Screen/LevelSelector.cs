using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSelector : ScreenUI
{
	public static Action OnBack;
	public static Action<int> OnLevelSelected;
	
	[SerializeField] private List<LevelButton> _levelButtons = new List<LevelButton>();
	[SerializeField] private AnimatedButton _back;

	protected override void Start()
	{
		foreach (LevelButton levelButton in _levelButtons)
		{
			levelButton.OnPlay += () =>
			{
				OnLevelSelected?.Invoke(levelButton.LevelIndex);
			};
		}

		_back.OnPlay += Back;
	}

	private void Back()
	{
		OnBack?.Invoke();
	}

	protected override void OnDestroy()
	{
		foreach (LevelButton levelButton in _levelButtons)
		{
			levelButton.OnPlay -= () =>
			{
				OnLevelSelected?.Invoke(levelButton.LevelIndex);
			};
		}
	}
}
