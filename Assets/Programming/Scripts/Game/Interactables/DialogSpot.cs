using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpot : AnimatedObject, IInteractable
{
	[SerializeField] private GameObject _UI;

	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private TextMeshProUGUI _number;
	[SerializeField] private Image _checkMark;
	
	[SerializeField] private float _duration;

	public void Interacting()
	{
		StartCoroutine(Appear());
	}

	private IEnumerator Appear()
	{
		_UI.SetActive(true);
		LevelManager.Instance.Player.PauseMovement();
		
		float lTimer = 0;

		while (lTimer < _duration)
		{
			lTimer += Time.deltaTime;
			_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, (lTimer / _duration));
			_number.color = new Color(_number.color.r, _number.color.g, _number.color.b, (lTimer / _duration));
			_checkMark.color = new Color(_checkMark.color.r, _checkMark.color.b, _checkMark.color.b, (lTimer / _duration));
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator Disappear()
	{
		float lTimer = 0;

		while (lTimer < _duration)
		{
			lTimer += Time.deltaTime;
			_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1 - (lTimer / _duration));
			_number.color = new Color(_number.color.r, _number.color.g, _number.color.b, 1 - (lTimer / _duration));
			_checkMark.color = new Color(_checkMark.color.r, _checkMark.color.b, _checkMark.color.b, 1 - (lTimer / _duration));
			yield return new WaitForEndOfFrame();
		}
		
		_UI.SetActive(false);
		InputManager.Instance.UnlockInput();
	}

	public void CloseUI()
	{
		StartCoroutine(Disappear());
	}
}
