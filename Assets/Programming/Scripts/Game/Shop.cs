using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : AnimatedObject, IInteractable
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ParticleSystem _saveFX;

    public Vector3 spawnPoint {  get { return _spawnPoint.position; } }

    public void Interacting()
	{
        LevelManager.Level.SaveLevelProgression(this);
        _saveFX.Play();
    }

    public void DoSeedTransition() { m_animator.SetBool("SeedTransition", true); }
    public void DoCheeseTransition() { m_animator.SetBool("CheeseTransition", true); }
    public void DoAddTransition() { m_animator.SetBool("AddTransition", true); }

    public void CancelSeedTransition() { m_animator.SetBool("SeedTransition", false); }
    public void CancelCheeseTransition() { m_animator.SetBool("CheeseTransition", false); }
    public void CancelAddTransition() { m_animator.SetBool("AddTransition", false); }


    public void Cheese() { m_animator.SetTrigger("Cheese"); }
    public void Adds() { m_animator.SetTrigger("Adds"); }
}