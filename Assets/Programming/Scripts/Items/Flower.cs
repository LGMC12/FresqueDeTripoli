using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : HarvestableObject
{
	public static Action<Flower> onHarvest;

    private Picture _picture;

    public Picture Picture => _picture;

    private void Start()
    {
        _picture = GetComponentInParent<Picture>();
    }

    public override void OnCollisionWithPlayer()
    {
        base.OnCollisionWithPlayer();

        m_harvestParticles.transform.parent = _picture.transform;

        onHarvest?.Invoke(this);
    }
}
