using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Touch = UnityEngine.Touch;

public class ExploreModeInputManager : MonoBehaviour
{
	private static Vector3 _mouseLastPosition;
	private static Vector3 _mouseDeltaPosition;

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

	public static Vector3 MouseDeltaPosition
    {
		get => _mouseDeltaPosition;
    }

    private void Start()
    {
		_mouseLastPosition = Input.mousePosition;
    }

    private void Update()
    {
		_mouseDeltaPosition = _mouseLastPosition - Input.mousePosition;
		_mouseLastPosition = Input.mousePosition;
    }
}
