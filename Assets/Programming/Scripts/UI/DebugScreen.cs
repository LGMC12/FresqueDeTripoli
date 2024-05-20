using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
	[SerializeField] private GameObject _panel;
	[SerializeField] private ControllableObject _co;

	[SerializeField] private Slider _vMinSlider;
	[SerializeField] private Slider _hMinSlider;
	[SerializeField] private Slider _vMaxSlider;
	[SerializeField] private Slider _hMaxSlider;
	[SerializeField] private Slider _zoomSlider;
	[SerializeField] private Slider _zoomMinSlider;
	[SerializeField] private Slider _zoomMaxSlider;
	[SerializeField] private Slider _deltaSlider;

	private bool _isOpen = false;

	public void OpenOrClose()
	{
		_isOpen = !_isOpen;
		_panel.SetActive(_isOpen);
		_co.debugActive = _isOpen;
		ApplyValues();
	}

	public void ApplyValues()
	{
		_co.SetSpeeds(_vMinSlider.value, _vMaxSlider.value, _hMinSlider.value, _hMaxSlider.value,
			_zoomSlider.value, _zoomMinSlider.value, _zoomMaxSlider.value, _deltaSlider.value);
	}
}