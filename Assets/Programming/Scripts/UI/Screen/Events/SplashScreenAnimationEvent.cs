using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class SplashScreenAnimationEvent : UIScreenAnimationEvent
{
	public static Action OnClosed;
	public override void OnOpenAnimationDone()
	{
		OnClosed?.Invoke();
	}
}