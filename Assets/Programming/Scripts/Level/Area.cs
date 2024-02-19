using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
	[SerializeField] private ParticleSystem _unlockAllFX;
	[SerializeField] private ParticleSystem _unlockFX;
	[SerializeField] private ParticleSystem _FX;

	[SerializeField] private SpriteRenderer _renderer;

	[SerializeField] private Color _toUnlockColor;
	[SerializeField] private Color _unlockedColor;

	[SerializeField] private GameObject _pictureContainer;

	private bool isUnlocked = false;

	private int _nToUnlock;

    private void Start()
    {
		_renderer.color = _toUnlockColor;
		Picture.OnUnlock += Picture_Unlocked;
		_nToUnlock = _pictureContainer.GetComponentsInChildren<Picture>().Length;
	}

    private void Picture_Unlocked(Picture pPicture)
    {
        if (pPicture.Area == this)
        {
			if (--_nToUnlock <= 0) Unlock();
        }
    }

	public void OnPlayerCollision()
    {
		if (isUnlocked) return;

		UnlockAll();
		
	}

	private void UnlockAll()
    {
		Unlock();

		_unlockAllFX.Play();

		foreach (Picture picture in _pictureContainer.GetComponentsInChildren<Picture>())
		{
			picture.Unlock();
		}
	}

    private void Unlock()
	{
		_FX.Stop();

		_unlockFX.Play();

		isUnlocked = true;

		_renderer.color = _unlockedColor;
	}
}
