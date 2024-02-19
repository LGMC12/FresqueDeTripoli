using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private GameObject _ammo;
	[SerializeField] private Transform _cannon;


	[SerializeField] private float _fireCooldown;
	private float _fireTime;

	[SerializeField] private ParticleSystem _muzzleFlashFX;

	private void Shoot()
	{
		Ammo lAmmo = Instantiate(_ammo).GetComponent<Ammo>();

		lAmmo.transform.position = _cannon.position;
		lAmmo.transform.rotation = _cannon.rotation;
		lAmmo.Init(transform.right);

		_muzzleFlashFX.Play();
	}

	public void Fire()
    {
		if (_fireTime >= _fireCooldown)
		{
			Shoot();
			_fireTime = 0.0f;
		}
	}

    private void Update()
	{
		_fireTime += Time.deltaTime;
	}
}