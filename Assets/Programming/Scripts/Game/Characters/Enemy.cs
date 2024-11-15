using UnityEngine;


public class Enemy : MovableObject, IInteractable
{
    [SerializeField] private AudioSource _deathSfx;
    
    public void Interacting()
    {
        _deathSfx.Play();
    }
}