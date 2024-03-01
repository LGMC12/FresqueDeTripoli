using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Touch = UnityEngine.Touch;

public class ExploreModeInputManager : MonoBehaviour
{
    public static Touch TouchInput0
	{
		get
		{
			Touch lTouch;

			if (Input.touchCount > 0)
			{
				lTouch = Input.GetTouch(0);
			}
			else lTouch = new Touch();

			return lTouch;
		}
	}

	public static Touch TouchInput1
	{
		get
		{
			Touch lTouch;

			if (Input.touchCount > 1)
			{
				lTouch = Input.GetTouch(1);
			}
			else lTouch = new Touch();

			return lTouch;
		}
	}
}
