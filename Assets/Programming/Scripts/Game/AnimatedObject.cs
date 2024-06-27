using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Animator _animator;
}