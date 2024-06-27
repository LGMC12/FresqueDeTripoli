using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableObject
{
    private void Awake()
    {
		Level.InitPlayerPosition += InitPosition;
    }

    private void InitPosition(Vector3 pPosition)
    {
		transform.position = pPosition;
    }

	protected override void MoveAndRotate()
	{
		_rb.velocity = _direction * _speed;
	}

	private void SetDirection()
    {
        if (PlayModeInputManager.Instance.Direction != Vector2.zero)
        {
			_direction = PlayModeInputManager.Instance.Direction;
		}
	}

	void Update()
	{
		SetDirection();
	}

    private void LateUpdate()
    {
		MoveAndRotate();
    }

    private void OnTriggerEnter2D(Collider2D pCollided)
	{
		IInteractable pInteractable;

		if (pCollided.transform.parent.TryGetComponent(out pInteractable))
		{
			pInteractable.Interacting();
		}

	}

    private void OnCollisionEnter2D(Collision2D pCollided)
    {

	}

    private void OnDestroy()
	{
	}
}
