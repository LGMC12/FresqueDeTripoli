using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	[SerializeField] private GameObject _points;
	[SerializeField] private int _nMaxEnemies = 1;
	private int _nEnemies = 0;
	private List<DeadPixel> _enemyList = new List<DeadPixel>();

	public Transform AppearPoint
	{ get => _points.transform.GetChild(Random.Range(0, _points.transform.childCount)); }

	public bool CheckEnemyEntrance(DeadPixel pEnemy)
	{
		if (_nEnemies < _nMaxEnemies)
		{
			++_nEnemies;
			_enemyList.Add(pEnemy);
			return true;
		}

		return false;
	}

	public void EnemyTeleporting(DeadPixel pEnemy)
	{
		--_nEnemies;
		_enemyList.Remove(pEnemy);
	}
}