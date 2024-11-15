using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class LevelButton : AnimatedButton
{
	[Space(10)] [SerializeField] private bool _isLocked = true;
	[SerializeField] private int _levelIndex;

	public int LevelIndex => _levelIndex;
}