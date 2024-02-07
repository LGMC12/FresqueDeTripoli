using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableObject : MonoBehaviour
{
	[SerializeField] protected ParticleSystem m_harvestParticles;
	[SerializeField] private BoxCollider2D _collider;
	[SerializeField] private SpriteRenderer _renderer;

	public virtual void OnCollisionWithPlayer()
    {
		m_harvestParticles.Play();
		_renderer.color = new Color(1, 1, 1, 0);
		_collider.gameObject.SetActive(false);
	}
}
