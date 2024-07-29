using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EGamePhase
{
	Seeds, 
	Cheeses, 
	Adds,
}

public class Level : MonoBehaviour
{
	public static Action<Vector3> InitPlayerPosition;
	public static Action OnLevelClear;
	public static Action OnSeedPhaseComplete;

	protected EGamePhase m_phase = EGamePhase.Seeds;

	[Header("Harvestables")]
	[SerializeField] protected GameObject m_seeds;
	[SerializeField] protected GameObject m_cheeses;
	[SerializeField] protected GameObject m_adds;
	[SerializeField] protected GameObject m_flowers;

	[Header("Shops")]
	[SerializeField] private GameObject _startShop;
	[SerializeField] private List<GameObject> _shops = new List<GameObject>();

	[Header("Doors")]
	[SerializeField] protected List<GameObject> m_doorsList = new List<GameObject>();

	[Header("Background")]
	[SerializeField] protected SpriteRenderer m_baseBG;
	[SerializeField] protected SpriteRenderer m_openBG;
	[SerializeField] protected SpriteRenderer m_negatedBG;

	[Header("Enemies")]
	[SerializeField] protected GameObject m_enemies; 

	[Header("Pixels Zones")]
	[SerializeField] protected GameObject m_zones;
	protected List<Zone> m_zonesList = new List<Zone>();
	public List<Zone> ZonesList { get { return m_zonesList; } }

	[Header("Traps")]
	[SerializeField] protected GameObject m_hiddenTraps;
	[SerializeField] protected GameObject m_hiddableTraps;

	protected List<HarvestableObject> m_toDestroy = new List<HarvestableObject>();

	public Vector3 RespawnPoint {  get; protected set; }
	protected virtual void Awake()
	{
		HarvestableObject.OnHarvested += HarvestableObject_OnHarvested;
		Player.OnDeath += Player_OnDeath;

		m_zonesList = m_zones.GetComponentsInChildren<Zone>().ToList();
	}

	private void OnEnable()
	{
		m_totalSeeds = m_seedsCount = m_seeds.transform.childCount;
		m_totalCheeses = m_cheesesCount = m_cheeses.transform.childCount;
		m_totalAdds = m_addsCount = m_adds.transform.childCount;

		InitPlayerPosition?.Invoke(m_startShop.transform.position);

		m_enemies.SetActive(true);
	}

	private void Player_OnDeath()
	{
		foreach (HarvestableObject pHarvested in m_toDestroy)
		{
			pHarvested.Respawn();
		}

		m_toDestroy.Clear();

		m_seedsCount = m_seeds.transform.childCount; 
		m_cheesesCount = m_cheeses.transform.childCount;
		m_addsCount = m_adds.transform.childCount;
    }

	protected virtual void HarvestableObject_OnHarvested(HarvestableObject pHarvested)
	{
		m_toDestroy.Add(pHarvested);

		switch (pHarvested.HarvestableType)
		{
			case E_HarvestableType.Seed:
				--m_seedsCount;
					break;
			case E_HarvestableType.Cheese:
				--m_cheesesCount;
                break;
			case E_HarvestableType.Adds:
				--m_addsCount;
                break;
			default:
				break;
		}

	}

	{






	public virtual void	CheckNextPhase()
	{
		switch (m_phase)
		{
			case EGamePhase.Seeds:
				if (m_seeds.transform.childCount == 0) CheesePhase();
				break;
			case EGamePhase.Cheeses:
				if (m_cheeses.transform.childCount == 0) AddsPhase();
				break;
			case EGamePhase.Adds:
				if (m_adds.transform.childCount == 0) LevelClear();
				break;
			default:
				break;
		}
	}

	protected virtual void CheesePhase()
	{
		foreach (GameObject pDoor in m_doorsList)
		{
			pDoor.SetActive(false);
		}

		m_baseBG.gameObject.SetActive(false);
		m_openBG.gameObject.SetActive(true);

		m_phase = EGamePhase.Cheeses;

		OnSeedPhaseComplete?.Invoke();

    }

	protected virtual void AddsPhase()
	{
		m_phase = EGamePhase.Adds;

		m_openBG.gameObject.SetActive(false);
		m_negatedBG.gameObject.SetActive(true);

		m_hiddenTraps.gameObject.SetActive(true);
		m_hiddableTraps.gameObject.SetActive(false);
    }

	protected virtual void LevelClear()
	{
		print("LEVEL CLEAR");

		OnLevelClear?.Invoke();
	}

	protected virtual void OnDestroy()
	{
		HarvestableObject.OnHarvested -= HarvestableObject_OnHarvested;
		Player.OnDeath -= Player_OnDeath;
	}
}