using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovableObject
{
    [Header("Animations")]
    [SerializeField] private string _appearTrigger;
    [SerializeField] private string _disappearTrigger;
    protected override void MoveAndRotate()
    {
        
    }
}