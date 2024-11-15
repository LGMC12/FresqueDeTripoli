using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	[SerializeField] private List<Transform> _points = new List<Transform>();

	public List<Transform> Points { get => _points; }

	private void Awake()
	{
		for (int i = 0; i < transform.childCount - 1; i++)
		{
			_points.Add(transform.GetChild(i));
		}

		_points.Add(transform);
	}
}
