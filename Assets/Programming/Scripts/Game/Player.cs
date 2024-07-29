using DG.Tweening;
using System;
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
		IInteractable pInteractable;

		if (pCollided.transform.parent.TryGetComponent(out pInteractable))
		{
			pInteractable.Interacting();
		}
	}

	private void OnCollisionEnter2D(Collision2D pCollided)
	{
		Enemy pEnemy = pCollided.collider.gameObject.transform.GetComponentInParent<Enemy>();

		if (pEnemy != null) Death();
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
