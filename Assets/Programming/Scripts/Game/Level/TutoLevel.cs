using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoLevel : Level
{
    public override void CheckNextPhase()
    {
        switch (m_phase)
        {
            case EGamePhase.Seeds:
                if (m_seeds.transform.childCount == 0) CheesePhase();
                else Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowSeed(m_seedsCount, m_totalSeeds));
                break;
            case EGamePhase.Cheeses:
                if (m_cheeses.transform.childCount == 0) LevelClear();
                else Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowCheese(m_cheesesCount, m_totalCheeses));
                break;
            default:
                break;
        }
    }

    protected override void CheesePhase()
    {
        foreach (GameObject pDoor in m_doorsList)
        {
            pDoor.SetActive(false);
        }

        m_baseBG.gameObject.SetActive(false);
        m_negatedBG.gameObject.SetActive(true);

        m_phase = EGamePhase.Cheeses;

        OnSeedPhaseComplete?.Invoke();

        Hud.Instance.ShowCheeses();
        Hud.Instance.ShopUI.StartCoroutine(Hud.Instance.ShopUI.ShowCheese(m_cheesesCount, m_totalCheeses));

        foreach (Shop pShop in m_shopsList)
        {
            pShop.Cheese();
        }
    }


}
