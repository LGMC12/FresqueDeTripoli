using System.Collections;
using UnityEngine;

public class Patroller : Enemy
{
	private Vector3 _start;
	private Vector3 _end;

	private PatrolPath _path;
	private int _currentPointIndex = 0;

	private float _ratio = 0;

	private float _duration;

	private PatrollerSpawner _spawner;

	[SerializeField] private float _speedMultiplier = 1;

	private Coroutine _coroutine;

	public void	Init(PatrolPath pPath, PatrollerSpawner pSpawner)
	{
		_path = pPath;

		_start = transform.position;
		_end = pPath.Points[0].position;

		_spawner = pSpawner;

		_coroutine = StartCoroutine(GoToNextPoint());
	}

	protected override void Move()
	{
		m_rb.position += m_direction * m_speed * Time.deltaTime * _speedMultiplier;
	}

	private IEnumerator GoToNextPoint()
	{
		m_direction = (_end - _start).normalized;

		float lDirAngle = Mathf.Atan2(m_direction.x, m_direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(lDirAngle, -Vector3.forward);

		if (lDirAngle == 90) transform.localScale = new Vector3(-50, 50, 1);
		else transform.localScale = Vector3.one * 50;

		while (_ratio < 1)
		{
			_ratio = (transform.position - _start).magnitude / (_end - _start).magnitude;
			Move();
			yield return new WaitForEndOfFrame();
		}

		NextPoint();

		yield return null;
	}

	private void NextPoint()
	{
		StopCoroutine(_coroutine);

		++_currentPointIndex;

		if (!(_currentPointIndex >= _path.Points.Count))
		{
			_start = _path.Points[_currentPointIndex - 1].position;
			_end = _path.Points[_currentPointIndex].position;

			_ratio = 0;

			_coroutine = StartCoroutine(GoToNextPoint());
		}
		else 
		{
			_spawner.PatrolFinished(this);
		}
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}
