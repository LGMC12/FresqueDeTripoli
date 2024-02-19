using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	[SerializeField] private float _speed = 25.0f;

	[SerializeField] private float _timeToDestroy;
	private float _destroyingTimer;

	private Vector3 _velocity;

	void Start()
	{
		
	}

	public void Init(Vector3 pDirection)
	{
		_velocity = pDirection;
	}

	private void OnTriggerEnter2D(Collider2D pCollider)
	{
		
	}


	void Update()
	{
		transform.position += _velocity * Time.deltaTime * _speed;

		_destroyingTimer += Time.deltaTime;
		if (_destroyingTimer >= _timeToDestroy) Destroy(gameObject);
	}
}
