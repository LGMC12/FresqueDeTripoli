using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayModeInputManager : MonoBehaviour
{
	private static PlayModeInputManager _instance;

	[SerializeField] private float _minDistanceForHorizontalScroll;
	[SerializeField] private float _maxDistanceForHorizontalScroll;
	[SerializeField] private float _minDistanceForVerticalScroll;
	[SerializeField] private float _maxDistanceForVerticalScroll;

	public static PlayModeInputManager Instance
	{
		get => _instance;
		private set { _instance = value; }
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		Instance = this;
	}
	private Touch TouchInput0
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

	public Vector3 Direction
	{
		get
		{
			Vector3 lDir = Vector3.zero;
#if UNITY_ANDROID

			Vector3 lDelta = TouchInput0.deltaPosition.normalized;

            if (Mathf.Abs(lDelta.x) > Mathf.Abs(lDelta.y))
            {
				lDir = new Vector3(Mathf.RoundToInt(TouchInput0.deltaPosition.normalized.x), 0, 0);
			}
            else
            {
				lDir = new Vector3(0, Mathf.RoundToInt(TouchInput0.deltaPosition.normalized.y), 0);
			}
#endif
#if UNITY_EDITOR
			lDir = new Vector3(Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.Q)),
				Convert.ToInt32(Input.GetKey(KeyCode.Z)) - Convert.ToInt32(Input.GetKey(KeyCode.S)), 0);

#endif
			return lDir;
		}
	}
}
