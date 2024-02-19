using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
	public static Action<Picture> OnUnlock;

	[SerializeField] private SpriteRenderer _pixelatedImage;
	[SerializeField] private SpriteRenderer _unpixelatedImage;

	[SerializeField] private GameObject _FlowerContainer;
	[SerializeField] private ParticleSystem _unlockFX;

	private Area _area;
	public Area Area => _area;

	private int nImageRemaining = 0;
	private int nTotalImage = 0;

	private void Start()
	{
		nTotalImage = _FlowerContainer.GetComponentsInChildren<Flower>().Length;
		nImageRemaining = nTotalImage;

		_area = GetComponentInParent<Area>();

		Flower.onHarvest += OnflowerHarvested;
	}

	private void OnflowerHarvested(Flower pFlower)
	{
		if (this == pFlower.Picture)
		{
			Destroy(pFlower.gameObject);
			--nImageRemaining;
			if (nImageRemaining <= 0) Unlock();
		}
	}

	public void Unlock()
	{
		_pixelatedImage.gameObject.SetActive(false);
		_unpixelatedImage.gameObject.SetActive(true);

		_unlockFX.Play();

		foreach (HarvestableObject flower in _FlowerContainer.GetComponentsInChildren<Flower>())
		{
			Destroy(flower.gameObject);
			--nImageRemaining;
		}

		OnUnlock?.Invoke(this);
	}
}
