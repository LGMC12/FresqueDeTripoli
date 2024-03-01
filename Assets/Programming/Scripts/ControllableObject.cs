using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : MonoBehaviour
{
	[Header("Scrolling Speed")]
	[SerializeField] private float _minVerticalSpeed = 1.0f;
	[SerializeField] private float _minHorizontalSpeed = 1.0f;
	[SerializeField] private float _maxVerticalSpeed = 1.0f;
	[SerializeField] private float _maxHorizontalSpeed = 1.0f;

	[Header("Zoom Parameters")]
	[SerializeField] private float _zoomValue = 1.0f;
	[SerializeField] private float _minZoom = 0.0f;
	[SerializeField] private float _maxZoom = 1.0f;

	[Header("Distance for scrolling")]
	[SerializeField] private float _minDistanceForHorizontalScroll;
	[SerializeField] private float _maxDistanceForHorizontalScroll;
	[SerializeField] private float _minDistanceForVerticalScroll;
	[SerializeField] private float _maxDistanceForVerticalScroll;

	private float _lastDistance = 0.0f;
	private float _radius = 0.0f;

	private float currentVerticalSpeed;
	private float currentHorizontalSpeed;

	private void Scroll()
	{
		if (Input.touchCount != 1) return;

		currentVerticalSpeed = Mathf.Lerp(_minVerticalSpeed, _maxVerticalSpeed, _radius / (_maxZoom + Mathf.Abs(_minZoom)));
		currentHorizontalSpeed = Mathf.Lerp(_minHorizontalSpeed, _maxHorizontalSpeed, _radius / (_maxZoom + Mathf.Abs(_minZoom)));

		if (ExploreModeInputManager.TouchInput0.deltaPosition.x > _maxDistanceForHorizontalScroll ||
			ExploreModeInputManager.TouchInput0.deltaPosition.x < _minDistanceForHorizontalScroll)
		{
			transform.position +=
			new Vector3(ExploreModeInputManager.TouchInput0.deltaPosition.normalized.x, 0, 0)
			* Time.deltaTime
			* currentHorizontalSpeed;
		}

		if (ExploreModeInputManager.TouchInput0.deltaPosition.y > _maxDistanceForVerticalScroll ||
			ExploreModeInputManager.TouchInput0.deltaPosition.y < _minDistanceForVerticalScroll)
		{
			transform.position +=
			new Vector3(0, ExploreModeInputManager.TouchInput0.deltaPosition.normalized.y, 0)
			* Time.deltaTime
			* currentVerticalSpeed;
		}
	}

	private void Zoom()
	{
		if (Input.touchCount != 2) return;

		Touch lTouch0 = ExploreModeInputManager.TouchInput0;
		Touch lTouch1 = ExploreModeInputManager.TouchInput1;

		if (lTouch0.phase == TouchPhase.Moved && lTouch1.phase == TouchPhase.Moved)
		{
			float lCurrentDistance = Vector2.Distance(lTouch0.position, lTouch1.position);

			if (_lastDistance < lCurrentDistance)
			{
				_radius -= _zoomValue;
			}
			else if (_lastDistance > lCurrentDistance)
			{
				_radius += _zoomValue;
			}

			_radius = Mathf.Clamp(_radius, _minZoom, _maxZoom);
			transform.position = Vector3.forward * _radius;

			_lastDistance = lCurrentDistance;
		}
	}

	void Update()
	{
		Zoom();
		Scroll();
	}
}
