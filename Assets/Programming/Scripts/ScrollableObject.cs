using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
    private RectTransform _transf;
	[SerializeField] private float _minDistanceForVerticalScroll;
	[SerializeField] private float _maxDistanceForVerticalScroll;
	[SerializeField] private float _verticalSpeed = 1200.0f;

	[SerializeField] private float maxHeight;
	[SerializeField] private float minHeight;
    private Vector3 _startPosition;


    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = _startPosition;
    }

    void Update()
    {
        if (Input.touchCount != 1 && !Input.GetMouseButton(0)) return;

        if (ExploreModeInputManager.TouchInput0.deltaPosition.y > _maxDistanceForVerticalScroll ||
            ExploreModeInputManager.TouchInput0.deltaPosition.y < _minDistanceForVerticalScroll)
        {
            transform.position +=
            new Vector3(0, ExploreModeInputManager.TouchInput0.deltaPosition.normalized.y, 0)
            * Time.deltaTime
            * _verticalSpeed;
        }

        if (Input.GetMouseButton(0) &&
                (ExploreModeInputManager.MouseDeltaPosition.y < _minDistanceForVerticalScroll ||
                ExploreModeInputManager.MouseDeltaPosition.y > _maxDistanceForVerticalScroll))
        {
            transform.position +=
            new Vector3(0, -ExploreModeInputManager.MouseDeltaPosition.normalized.y, 0)
            * Time.deltaTime
            * _verticalSpeed;
        }

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
    }
}