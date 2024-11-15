using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideInGame : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _renderer;
	[SerializeField] private bool _hiddenInGame = true;

	private void Awake()
	{
		if(_hiddenInGame) _renderer.enabled = false;
	}
}
