using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Player Movement")]
	[SerializeField] private float _speed = 1.0f;

	[SerializeField] private float _accelerationDuration = 1.0f;
	private float _accelerationTime = 0.0f;
	[SerializeField] private AnimationCurve _accelerationCurve;

	[Space]
	[Header("Firing")]
	[SerializeField] private Weapon _weapon;

	[SerializeField] private GameObject _renderer;

	void Start()
	{
	}

	private void Shoot()
	{
		_weapon.Fire();
	}

	private void MoveAndRotate()
	{
		transform.position += InputManager.Instance.Direction * Time.deltaTime * 5.0f;

		if (InputManager.Instance.Direction != Vector3.zero)
		{
			_renderer.transform.rotation = Quaternion.Euler(0, 0, 
				Mathf.Atan2(InputManager.Instance.Direction.y, InputManager.Instance.Direction.x) * Mathf.Rad2Deg);
		}

	}


	void Update()
	{
		MoveAndRotate();
		if (InputManager.Instance.Action) Shoot();
	}

	private void OnTriggerEnter2D(Collider2D pCollided)
	{
		HarvestableObject lHO;
		Area lArea;

		if (pCollided.gameObject.transform.parent.TryGetComponent<HarvestableObject>(out lHO))
		{
			lHO.OnCollisionWithPlayer();
		}
		else if (pCollided.gameObject.transform.parent.TryGetComponent<Area>(out lArea))
		{
			lArea.OnPlayerCollision();
		}
	}

	private void OnDestroy()
	{
	}
}
