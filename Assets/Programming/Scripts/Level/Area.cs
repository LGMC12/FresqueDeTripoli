using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
	[SerializeField] private ParticleSystem _unlockFX;
	[SerializeField] private ParticleSystem _FX;

	public void Unlock()
	{
		_FX.Stop();
		_unlockFX.Play();

		foreach (Picture picture in GetComponentsInChildren<Picture>())
		{
			picture.Unlock();
		}
	}
}
