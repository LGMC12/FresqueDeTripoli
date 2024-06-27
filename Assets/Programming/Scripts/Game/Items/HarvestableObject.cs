using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_HarvestableType
{
	Seed,
	Cheese,
	flower
}

public class HarvestableObject : AnimatedObject, IInteractable
{
	public static Action<HarvestableObject> OnHarvested;

	[SerializeField] protected ParticleSystem m_harvestParticles;

	[SerializeField] private E_HarvestableType _harvestableType;
	public E_HarvestableType HarvestableType
	{
		get => _harvestableType;
	}

    public void Interacting()
    {
        m_harvestParticles.Play();
        _renderer.color = new Color(1, 1, 1, 0);
        _collider.gameObject.SetActive(false);
        StartCoroutine(WaitUntilParticlesEnd());
    }

    public virtual IEnumerator WaitUntilParticlesEnd()
    {
        yield return new WaitWhile(() => m_harvestParticles.IsAlive(false));

		transform.parent = null;
        StopAllCoroutines();
        OnHarvested?.Invoke(this);
        yield break;
    }
}
