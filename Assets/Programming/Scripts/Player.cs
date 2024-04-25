using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Player Movement")]
	[SerializeField] private float _speed = 1.0f;

	private Vector3 _direction = Vector3.right;

	void Start()
	{
	}

	private void MoveAndRotate()
	{
		transform.position += _direction * Time.deltaTime * _speed;
	}

	private void SetDirection()
    {
        if (PlayModeInputManager.Instance.Direction != Vector3.zero)
        {
			_direction = PlayModeInputManager.Instance.Direction;
		}
	}

	void Update()
	{
		SetDirection();
		MoveAndRotate();
	}

	private void OnTriggerEnter2D(Collider2D pCollided)
	{
		HarvestableObject lHO;

		if (pCollided.gameObject.transform.parent.TryGetComponent<HarvestableObject>(out lHO))
		{
			lHO.OnCollisionWithPlayer();
		}
	}

	private void OnDestroy()
	{
	}
}
