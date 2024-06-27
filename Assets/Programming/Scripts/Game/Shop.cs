using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
	public void Interacting()
	{
		Level.CurrentLevel.CheckOpening();
	}
}