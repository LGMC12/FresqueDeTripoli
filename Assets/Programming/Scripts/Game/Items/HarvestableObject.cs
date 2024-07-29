using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_HarvestableType
{
	Seed,
	Cheese,
	Adds,
	Flower
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
		m_renderer.color *= new Color(1, 1, 1, 0);
		m_collider.gameObject.SetActive(false);

        OnHarvested?.Invoke(this);
    }

	public void Respawn()
    {
		m_renderer.color += new Color(0, 0, 0, 1);
        m_collider.gameObject.SetActive(true);
    }
}
