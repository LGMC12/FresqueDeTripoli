using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public static Action<Vector3> InitPlayerPosition;

	private static List<Level> _levels = new List<Level>();

	private static int _currentLevelID = 0;

	public static Level CurrentLevel
	{ get => _levels[_currentLevelID]; }

	[SerializeField] private int levelID;
	private bool isOpen = false;
	[Header("Harvestables")]
	[SerializeField] private GameObject _seedsOpen;
	[SerializeField] private GameObject _seedsClose;
	[SerializeField] private GameObject _cheesesOpen;
	[SerializeField] private GameObject _cheesesClose;
	[SerializeField] private GameObject _flowers;
	[Header("Shops")]
	[SerializeField] private GameObject _startShop;
	[SerializeField] private List<GameObject> _shops = new List<GameObject>();
	[Header("Obstacles")]
	[SerializeField] private List<GameObject> _doors = new List<GameObject>();
	[Header("Background")]
	[SerializeField] private SpriteRenderer _openBG;
	[SerializeField] private SpriteRenderer _closeBG;
	[Header("Enemies")]
	[SerializeField] private List<Enemy> _enemies = new List<Enemy>();


	private void Awake()
	{
		HarvestableObject.OnHarvested += HarvestableObject_OnHarvested_Close;

		_levels.Add(this);
	}

	private void Start()
	{
		InitPlayerPosition?.Invoke(_startShop.transform.position);

		_seedsOpen.SetActive(false);
		_cheesesOpen.SetActive(false);
	}

	public void	CheckOpening()
	{
		if (_seedsClose.transform.childCount == 0 && _cheesesClose.transform.childCount == 0) OpenLevel();
	}

	private void HarvestableObject_OnHarvested_Close(HarvestableObject pHarvested)
	{
		switch (pHarvested.HarvestableType)
		{
			case E_HarvestableType.Seed:

				break;

			case E_HarvestableType.Cheese:

				break;

			default:
				break;
		}

		DestroyImmediate(pHarvested);
	}

	private void HarvestableObject_OnHarvested_Open(HarvestableObject pHarvested)
	{
		switch (pHarvested.HarvestableType)
		{
			case E_HarvestableType.Seed:

			break;

			case E_HarvestableType.Cheese:

			break;

			default:
				break;
		}

		DestroyImmediate(pHarvested);

		if (_seedsOpen.transform.childCount == 0 && _cheesesOpen.transform.childCount == 0 && _flowers.transform.childCount == 0)
		{
			print("Level clear !");
		}
	}

	private void OpenLevel()
	{
		print("Open doors");
		HarvestableObject.OnHarvested += HarvestableObject_OnHarvested_Open;
		HarvestableObject.OnHarvested -= HarvestableObject_OnHarvested_Close;

		foreach (GameObject pDoor in _doors)
		{
			pDoor.SetActive(false);
		}

		_closeBG.gameObject.SetActive(false);
		_openBG.gameObject.SetActive(true);

		_seedsOpen.SetActive(true);
		_cheesesOpen.SetActive(true);
	}

	private void OnDestroy()
	{
		HarvestableObject.OnHarvested -= HarvestableObject_OnHarvested_Open;
	}
}