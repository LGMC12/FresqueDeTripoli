using System.Collections.Generic;
using UnityEngine;

public class PatrollerSpawner : MonoBehaviour
{
	[SerializeField] private List<PatrolPath> patrolPaths = new List<PatrolPath>();

	[SerializeField] private GameObject _patroller;

	[SerializeField] private int _maxSpawnCount = 1;
	private int _spawnCount = 0;

	[SerializeField] private float _spawnDelay = 2;
	private float _spawnTime = 0;

	private void Update()
	{
		if(_spawnTime >= _spawnDelay) Spawn();
		else _spawnTime += Time.deltaTime;
	}

	private void Spawn()
	{
		if (_spawnCount < _maxSpawnCount)
		{
			int lPathIndex = Random.Range(0, patrolPaths.Count);

			Patroller lPatroller = Instantiate(_patroller).GetComponent<Patroller>();
			lPatroller.transform.position = transform.position;
			lPatroller.Init(patrolPaths[lPathIndex], this);
			++_spawnCount;
		}

		_spawnTime -= _spawnDelay;
	}

	public void PatrolFinished(Patroller pPatroller)
	{
		Destroy(pPatroller.gameObject);
		--_spawnCount;
	}
}