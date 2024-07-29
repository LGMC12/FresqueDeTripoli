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
	[SerializeField] protected Shop m_startShop;
	[SerializeField] protected List<Shop> m_shopsList = new List<Shop>();

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

	protected int m_seedsCount = 0;
	protected int m_cheesesCount = 0;
	protected int m_addsCount = 0;
	
	protected int m_totalSeeds = 0;
	protected int m_totalCheeses = 0;
	protected int m_totalAdds = 0;


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

		Hud.Instance.SetMax(m_totalSeeds, m_totalCheeses, m_totalAdds);
		Hud.Instance.UpdateTxt(m_seedsCount, m_cheesesCount, m_addsCount);
		Hud.Instance.OnSaved(m_seedsCount, m_cheesesCount, m_addsCount);

		InitPlayerPosition?.Invoke(m_startShop.transform.position);

		m_enemies.SetActive(true);

		Hud.Instance.Start();
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

		Hud.Instance.UpdateTxt(m_seedsCount, m_cheesesCount, m_addsCount);

        switch (m_phase)
        {
            case EGamePhase.Seeds:
                foreach (Shop pShop in m_shopsList)
                {
                    pShop.CancelSeedTransition();
                }
                break;
            case EGamePhase.Cheeses:
                foreach (Shop pShop in m_shopsList)
                {
                    pShop.CancelCheeseTransition();
                }
                break;
            case EGamePhase.Adds:
                foreach (Shop pShop in m_shopsList)
                {
                    pShop.CancelAddTransition();
                }
                break;
            default:
                break;
        }
    }

	protected virtual void HarvestableObject_OnHarvested(HarvestableObject pHarvested)
	{
		m_toDestroy.Add(pHarvested);

		switch (pHarvested.HarvestableType)
		{
			case E_HarvestableType.Seed:
				--m_seedsCount;
				if (m_seedsCount == 0)
				{
					foreach (Shop pShop in m_shopsList)
					{
						pShop.DoSeedTransition();
					}
				}
					break;
			case E_HarvestableType.Cheese:
				--m_cheesesCount;
                if (m_cheesesCount == 0)
                {
                    foreach (Shop pShop in m_shopsList)
                    {
                        pShop.DoCheeseTransition();
                    }
                }
                break;
			case E_HarvestableType.Adds:
				--m_addsCount;
                if (m_addsCount == 0)
                {
                    foreach (Shop pShop in m_shopsList)
                    {
                        pShop.DoAddTransition();
                    }
                }
                break;
			default:
				break;
		}

		Hud.Instance.UpdateTxt(m_seedsCount, m_cheesesCount, m_addsCount);
	}

	public virtual void SaveLevelProgression(Shop pShop)
	{
		HarvestableObject pHarvested;

		for (int i = m_toDestroy.Count - 1; i >= 0; i--)
		{
			pHarvested = m_toDestroy[i];
			pHarvested.transform.parent = null;
			Destroy(pHarvested.gameObject);
		}

		m_toDestroy.Clear();

		Hud.Instance.OnSaved(m_seedsCount, m_cheesesCount, m_addsCount);

		RespawnPoint = pShop.spawnPoint;

		LevelManager.Instance.Player.PauseMovement();
		CheckNextPhase();
	}

	public virtual void	CheckNextPhase()
	{
		switch (m_phase)
		{
			case EGamePhase.Seeds:
				if (m_seeds.transform.childCount == 0) CheesePhase();
				else Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowSeed(m_seedsCount, m_totalSeeds));
				break;
			case EGamePhase.Cheeses:
				if (m_cheeses.transform.childCount == 0) AddsPhase();
				else Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowCheese(m_cheesesCount, m_totalCheeses));
				break;
			case EGamePhase.Adds:
				if (m_adds.transform.childCount == 0) LevelClear();
				else Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowAdds(m_addsCount, m_totalAdds));
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

		Hud.Instance.ShowCheeses(); 
		Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowCheese(m_cheesesCount, m_totalCheeses));

        foreach (Shop pShop in m_shopsList)
        {
            pShop.Cheese();
        }
    }

	protected virtual void AddsPhase()
	{
		m_phase = EGamePhase.Adds;

		m_openBG.gameObject.SetActive(false);
		m_negatedBG.gameObject.SetActive(true);

		m_hiddenTraps.gameObject.SetActive(true);
		m_hiddableTraps.gameObject.SetActive(false);

		Hud.Instance.ShowAdds(); 
		Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowAdds(m_addsCount, m_totalAdds));

        foreach (Shop pShop in m_shopsList)
        {
            pShop.Adds();
        }
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