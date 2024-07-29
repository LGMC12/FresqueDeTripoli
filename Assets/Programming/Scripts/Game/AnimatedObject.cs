using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    [SerializeField] protected Collider2D m_collider;
    [SerializeField] protected SpriteRenderer m_renderer;
    [SerializeField] protected Animator m_animator;
}