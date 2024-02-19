using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private DPad _dPad;
	[SerializeField] private FloatingJoystick _joystick;
	[SerializeField] private ActionButton _actionButton;

	[SerializeField] private bool usingJoystick = false;

	private static InputManager _instance;

	public static InputManager Instance
	{
		get => _instance;
		private set { _instance = value; }
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		Instance = this;
	}


	private void Start()
	{
		_dPad.gameObject.SetActive(!usingJoystick);
		_joystick.gameObject.SetActive(usingJoystick);
	}

	public Touch TouchInput0
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
			if (usingJoystick) return new Vector3(Joystick.Input.x, Joystick.Input.y, 0);
			return new Vector3(Convert.ToInt32(DPad.rightInput) - Convert.ToInt32(DPad.leftInput),
				Convert.ToInt32(DPad.upInput) - Convert.ToInt32(DPad.downInput), 0);
		}
	}

	public bool Action => _actionButton.IsClicked;
}
