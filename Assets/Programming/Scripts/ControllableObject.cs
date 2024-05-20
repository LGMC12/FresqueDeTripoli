using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableObject : MonoBehaviour
{
	[Header("Scrolling Speed")]
	[SerializeField] private float _minVerticalSpeed = 15.0f;
	[SerializeField] private float _minHorizontalSpeed = 10.0f;
	[SerializeField] private float _maxVerticalSpeed = 300.0f;
	[SerializeField] private float _maxHorizontalSpeed = 350.0f;

	[Header("Zoom Parameters")]
	[SerializeField] private float _zoomValue = 10.0f;
	[SerializeField] private float _minZoom = 40.0f;
	[SerializeField] private float _maxZoom = 500.0f;

	[Header("Distance for scrolling")]
	[SerializeField] private float _minDistanceForHorizontalScroll;
	[SerializeField] private float _maxDistanceForHorizontalScroll;
	[SerializeField] private float _minDistanceForVerticalScroll;
	[SerializeField] private float _maxDistanceForVerticalScroll;

	private Vector3 _startposition;

	private float _lastDistance = 0.0f;
	private float _radius = 0.0f;

	private float currentVerticalSpeed;
	private float currentHorizontalSpeed;

	public bool debugActive = false;

    private void Awake()
    {
		_startposition = transform.position;
		_radius = Mathf.Clamp(transform.position.z, _minZoom, _maxZoom);
	}

    private void OnEnable()
    {
		transform.position = _startposition;
		_radius = Mathf.Clamp(transform.position.z, _minZoom, _maxZoom);
	}

    public void SetSpeeds(float vMinSpeed, float vMaxSpeed, float hMinSpeed, float hMaxSpeed, float zoom, float zoomMin, float zoomMax, float delta)
	{
		_minVerticalSpeed = vMinSpeed;
		_minHorizontalSpeed = hMinSpeed;

		_maxVerticalSpeed = vMaxSpeed;
		_maxHorizontalSpeed = hMaxSpeed;

		_zoomValue = zoom;
		_minZoom = zoomMin;
		_maxZoom = zoomMax;

		_minDistanceForHorizontalScroll = _maxDistanceForHorizontalScroll = _minDistanceForVerticalScroll = _maxDistanceForVerticalScroll = delta;
	}

	private void Scroll()
	{
		if ((Input.touchCount != 1 && !Input.GetMouseButton(0)) || debugActive) return;

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

		if (Input.GetMouseButton(0) &&
				(ExploreModeInputManager.MouseDeltaPosition.y < _minDistanceForVerticalScroll ||
				ExploreModeInputManager.MouseDeltaPosition.y > _maxDistanceForVerticalScroll))
		{
			transform.position +=
			new Vector3(0, -ExploreModeInputManager.MouseDeltaPosition.normalized.y, 0)
			* Time.deltaTime
			* currentVerticalSpeed;
		}



		if (Input.GetMouseButton(0) &&
				(ExploreModeInputManager.MouseDeltaPosition.x < _minDistanceForHorizontalScroll ||
				ExploreModeInputManager.MouseDeltaPosition.x > _maxDistanceForHorizontalScroll))
		{
			transform.position +=
			new Vector3(-ExploreModeInputManager.MouseDeltaPosition.normalized.x, 0, 0)
			* Time.deltaTime
			* currentHorizontalSpeed;
		}
	}

	private void Zoom()
	{
#if UNITY_ANDROID
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

			_lastDistance = lCurrentDistance;
		}
#else

		_radius += -Input.mouseScrollDelta.y * _zoomValue;
#endif

		_radius = Mathf.Clamp(_radius, _minZoom, _maxZoom);
		transform.position = new Vector3(transform.position.x, transform.position.y, _radius);
	}

	void Update()
	{
		Zoom();
		Scroll();
	}
}
