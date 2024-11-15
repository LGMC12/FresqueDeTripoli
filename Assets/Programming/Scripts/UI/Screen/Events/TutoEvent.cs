using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEvent : UIScreenAnimationEvent
{
	public static Action OnOpenAnimDone;
	
	public override void OnOpenAnimationDone()
	{
		OnOpenAnimDone?.Invoke();
	}
}
