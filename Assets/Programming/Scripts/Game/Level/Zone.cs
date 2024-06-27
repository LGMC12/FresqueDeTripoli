using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	[SerializeField] private Transform _appearPoint;
	[SerializeField] private int _nMaxEnemies = 1;
	private int _nEnemies = 0;
	private List<Enemy> _enemyList = new List<Enemy>();

    private void Start()
    {
		
    }
}