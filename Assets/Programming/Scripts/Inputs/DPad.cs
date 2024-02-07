using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DPad : MonoBehaviour
{
	[SerializeField] private DPadButton _rightButton;
	[SerializeField] private DPadButton _leftButton;
	[SerializeField] private DPadButton _upButton;
	[SerializeField] private DPadButton _downButton;

	public static bool rightInput = false;
	public static bool leftInput = false;
	public static bool upInput = false;
	public static bool downInput = false;

	private Vector2 _axisInput;

	public Vector2 AxisInput
    {
		get => _axisInput;
		set
        {
			_axisInput = value;

        }
    }

	void Start()
	{

	}


	void Update()
	{
		rightInput = _rightButton.IsClicked;
		leftInput = _leftButton.IsClicked;
		upInput = _upButton.IsClicked;
		downInput = _downButton.IsClicked;
	}
}
