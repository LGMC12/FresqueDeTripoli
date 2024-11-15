using UnityEngine;

public class MovableObject : AnimatedObject
{
    [Header("Movement")]
    [SerializeField] protected float m_speed = 1.0f;
    [SerializeField] protected Rigidbody2D m_rb;
    protected Vector2 m_direction = Vector2.right;

    protected virtual void Move() { }
}