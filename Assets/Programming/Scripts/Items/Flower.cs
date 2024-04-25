using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : HarvestableObject
{
	public static Action<Flower> onHarvest;

    private void Start()
    {

    }

    public override void OnCollisionWithPlayer()
    {
        base.OnCollisionWithPlayer();
        onHarvest?.Invoke(this);
    }
}
