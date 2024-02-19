using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Button _button;

	public bool IsClicked = false;

	public void OnPointerUp(PointerEventData pEventData)
	{
		_button.OnPointerUp(pEventData);
		IsClicked = false;
	}

	public void OnPointerDown(PointerEventData pEventData)
	{
		_button.OnPointerDown(pEventData);
		IsClicked = true;
	}
}
