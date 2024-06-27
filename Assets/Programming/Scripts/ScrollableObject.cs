using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
	private RectTransform _transf;
	
	[SerializeField] private RectTransform _top;
	[SerializeField] private RectTransform _bottom;
	[SerializeField] private float _verticalSpeed = 1200.0f;

	private float minHeight;
	public Vector2 _startPosition;

	private float _yDelda;


	private void Awake()
	{
		_transf = GetComponent<RectTransform>();
		_startPosition = _transf.anchoredPosition;
	}

	private void OnEnable()
	{
		_transf.anchoredPosition = _startPosition;
		minHeight = Camera.main.WorldToViewportPoint(_top.position).y;
		print(minHeight);
	}

	void Update()
	{
		_yDelda = ExploreModeInputManager.TouchInput0.deltaPosition.normalized.y;

        if ((Camera.main.WorldToViewportPoint(_bottom.position).y >= 0 && _yDelda > 0)
			|| (Camera.main.WorldToViewportPoint(_top.position).y <= minHeight && _yDelda < 0)) return;

        if (Input.touchCount == 1)
		{
			_transf.anchoredPosition += new Vector2(0, _yDelda) * Time.deltaTime * _verticalSpeed;
		}
    }
}