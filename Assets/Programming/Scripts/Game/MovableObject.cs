using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : AnimatedObject
{
    [Header("Player Movement")]
    [SerializeField] protected float _speed = 1.0f;
    [SerializeField] protected Rigidbody2D _rb;
    protected Vector2 _direction = Vector2.right;

    protected virtual void MoveAndRotate() { }
}