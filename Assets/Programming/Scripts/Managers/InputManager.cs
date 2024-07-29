using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
	private static InputManager _instance;

	[SerializeField] private float _minDistanceForHorizontalScroll;
	[SerializeField] private float _maxDistanceForHorizontalScroll;
	[SerializeField] private float _minDistanceForVerticalScroll;
	[SerializeField] private float _maxDistanceForVerticalScroll;

	[SerializeField] private bool _blockInput = false;

	public static InputManager Instance
	{
		get => _instance;
		private set { _instance = value; }
	}

	private void Awake()
	{
		Instance = this;
	}

	public void BlockInput() { _blockInput = true; }
	public void UnlockInput() { _blockInput = false; }

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

	public Vector2 Direction
	{
		get
		{
			Vector2 lDir = Vector2.zero;

			if (_blockInput) return lDir;

			Vector2 lDelta = TouchInput0.deltaPosition.normalized;

			if (Input.touchCount == 0)
			{
				lDir = new Vector2(Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.Q)),
					Convert.ToInt32(Input.GetKey(KeyCode.Z)) - Convert.ToInt32(Input.GetKey(KeyCode.S)));

				if (Convert.ToInt32(Input.GetKey(KeyCode.D)) != 0) lDir = new Vector2(1, 0);
				else if (Convert.ToInt32(Input.GetKey(KeyCode.Q)) != 0) lDir = new Vector2(-1, 0);
                else if (Convert.ToInt32(Input.GetKey(KeyCode.Z)) != 0) lDir = new Vector2(0, 1);
                else if (Convert.ToInt32(Input.GetKey(KeyCode.S)) != 0) lDir = new Vector2(0, -1);
            }
			else if (Mathf.Abs(lDelta.x) > Mathf.Abs(lDelta.y))
			{
				lDir = new Vector2(Mathf.RoundToInt(TouchInput0.deltaPosition.normalized.x), 0);
			}
			else
			{
				lDir = new Vector2(0, Mathf.RoundToInt(TouchInput0.deltaPosition.normalized.y));
			}

			return lDir;
		}
	}
}
