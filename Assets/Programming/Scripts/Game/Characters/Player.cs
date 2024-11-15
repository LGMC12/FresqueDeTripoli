using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Player : MovableObject
{
	public static Action OnDeath;

	[Header("Animations trigger")]
	[SerializeField] private string _upTrigger;
	[SerializeField] private string _downTrigger;
	[SerializeField] private string _leftTrigger;
	[SerializeField] private string _rightTrigger;
	[SerializeField] private string _deathTrigger;
	[SerializeField] private string _reviveTrigger;

	[Header("Animators")]
	[SerializeField] private RuntimeAnimatorController _baseAnimator;
	[SerializeField] private RuntimeAnimatorController _seedAnimator;

	[SerializeField] private float _minAngle;
	[SerializeField] private float _maxAngle;

	[SerializeField] private List<float> _anglesList = new List<float>();

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
		if (InputManager.Instance.Direction.magnitude == 0) m_rb.velocity = m_direction * m_speed;
		else m_rb.velocity = m_direction * m_speed * InputManager.Instance.Direction.magnitude;
	}

	private void SetDirection()
	{
		if (InputManager.Instance.Direction == Vector2.zero) return;

		if (InputManager.Instance.Direction != Vector2.zero)
		{
			m_direction = InputManager.Instance.Direction;
		}

		float lDirAngle = Snapping.Snap((Mathf.Atan2(m_direction.x, m_direction.y) * Mathf.Rad2Deg + 360) % 360, 45);

        if (lDirAngle >= _minAngle && lDirAngle <= _maxAngle) transform.localScale = new Vector3(-40, 40, 1);
		else transform.localScale = new Vector3(40, 40, 1);
        
        transform.rotation = Quaternion.AngleAxis(lDirAngle, -Vector3.forward);
        
        lDirAngle *= Mathf.Deg2Rad;
        m_direction = new Vector2(Mathf.Sin(lDirAngle), Mathf.Cos(lDirAngle));
    }

	public void PauseMovement()
	{
		InputManager.Instance.BlockInput();
		m_direction = Vector2.zero;
	}

	public void ResetAnimator()
	{
		m_animator.runtimeAnimatorController = _baseAnimator;
	}

	private void SetSeedAnimator()
	{
		m_animator.runtimeAnimatorController = _seedAnimator;
	}


	private void Update()
	{
		SetDirection();
		Move();
	}

	private void OnTriggerEnter2D(Collider2D pCollided)
	{
		if (pCollided.transform.parent.TryGetComponent(out IInteractable lInteractable))
		{
			lInteractable.Interacting();
		}
	}

	private void OnCollisionEnter2D(Collision2D pCollided)
	{
		Enemy lEnemy = pCollided.collider.gameObject.transform.GetComponentInParent<Enemy>();

		if (lEnemy != null)
		{
			Death();
			lEnemy.Interacting();
		}
	}

	private void Death()
	{
		PauseMovement();

		m_collider.gameObject.SetActive(false);

		m_animator.SetTrigger(_deathTrigger);

		OnDeath?.Invoke();
	}

	public void Dead()
	{
		transform.DOMove(LevelManager.Level.RespawnPoint, 0.2f).OnComplete(() => { m_animator.SetTrigger(_reviveTrigger); });
		transform.DOMove(LevelManager.Level.RespawnPoint, 0f).SetDelay(1.2f).OnComplete(() => { Revive(); });
	}

	public void Revive()
	{
		InputManager.Instance.UnlockInput();
		m_collider.gameObject.SetActive(true); 
		m_animator.SetTrigger(_rightTrigger);
	}

	private void OnDestroy()
	{
		Level.InitPlayerPosition -= InitPosition;
		Level.OnSeedPhaseComplete -= SetSeedAnimator;
	}
}
