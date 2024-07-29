using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableObject
{
    private void Awake()
    {
	private void Awake()
	{
		Level.InitPlayerPosition += InitPosition;
		Level.OnSeedPhaseComplete += SetSeedAnimator;
	}

	private void InitPosition(Vector3 pPosition)
	{
		InputManager.Instance.BlockInput();
		transform.position = pPosition;
		InputManager.Instance.UnlockInput();
	}

	protected override void Move()
	{
		m_rb.velocity = m_direction * m_speed;
	}

	private void SetDirection()
	{
		if (InputManager.Instance.Direction == Vector2.zero) return;

		if (InputManager.Instance.Direction != Vector2.zero)
		{
			m_direction = InputManager.Instance.Direction;
		}


		if (m_direction.x >= 0 && m_direction.y == 0) m_animator.SetTrigger(_rightTrigger);
		if (m_direction.x <= 0 && m_direction.y == 0) m_animator.SetTrigger(_leftTrigger);
		if (m_direction.x == 0 && m_direction.y >= 0) m_animator.SetTrigger(_upTrigger);
		if (m_direction.x == 0 && m_direction.y <= 0) m_animator.SetTrigger(_downTrigger);
	}

	void Update()
	{
		SetDirection();
	}


	private void Update()
	{
		SetDirection();
		Move();
	}
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
