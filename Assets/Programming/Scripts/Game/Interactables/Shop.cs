using System;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : AnimatedObject, IInteractable
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ParticleSystem _saveFX;

    [SerializeField] private AudioSource _saveSeedsSFX;
    [SerializeField] private AudioSource _saveCheesesSFX;
    [SerializeField] private AudioSource _saveAddsSFX;
    private AudioSource _saveSFX;

    public Vector3 spawnPoint {  get { return _spawnPoint.position; } }

    public void Interacting()
	{
        LevelManager.Level.SaveLevelProgression(this);
        Hud.Instance.SaveAnim();
        _saveFX.Play();
        _saveSFX.Play();
    }

    private void Awake()
    {
	    _saveSFX = _saveSeedsSFX;
    }

    public void DoSeedTransition() { m_animator.SetBool("SeedTransition", true); }
    public void DoCheeseTransition() { m_animator.SetBool("CheeseTransition", true); }
    public void DoAddTransition() { m_animator.SetBool("AddTransition", true); }

    public void CancelSeedTransition() { m_animator.SetBool("SeedTransition", false); }
    public void CancelCheeseTransition() { m_animator.SetBool("CheeseTransition", false); }
    public void CancelAddTransition() { m_animator.SetBool("AddTransition", false); }


    public void Cheese() 
    { 
	    m_animator.SetTrigger("Cheese");
	    _saveSFX = _saveCheesesSFX;
    }

    public void Adds()
    {
	    m_animator.SetTrigger("Adds");
	    _saveSFX = _saveAddsSFX;
    }
}