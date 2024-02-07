using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector3 _velocity = Vector3.zero;

	void Start()
	{
	}

	private void Move()
	{
		//transform.position += InputManager.Direction * 5f * Time.deltaTime;
		if (DPad.downInput)
		{
			_velocity += Vector3.down;
		}
		else if (DPad.upInput)
		{
			_velocity += Vector3.up;
		}
		if (DPad.leftInput)
		{
			_velocity += Vector3.left;
		}
		else if (DPad.rightInput)
		{
			_velocity += Vector3.right;
		}

		transform.position += _velocity * Time.deltaTime * 5.0f;
		_velocity = Vector3.zero;
	}


	void Update()
	{
		Move();	
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
