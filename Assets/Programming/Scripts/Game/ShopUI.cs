using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject _seeds;
    [SerializeField] private GameObject _cheeses;
    [SerializeField] private GameObject _adds;

    [SerializeField] private Image _panel;
    [SerializeField] private Image _arrow;

    [SerializeField] private TextMeshProUGUI _seedsTxt;
    [SerializeField] private TextMeshProUGUI _cheesesTxt;
    [SerializeField] private TextMeshProUGUI _addsTxt;

    [SerializeField] private Button _resumeButton;

    private void Start()
    {
        _panel.color *= new Color(1, 1, 1, 0);
        _arrow.color *= new Color(1, 1, 1, 0);
    }

    public IEnumerator ShowSeed(int pSeeds, int pMaxSeeds)
    {
        yield return new WaitForSeconds(0.15f);

        _panel.color *= new Color(1, 1, 1, 0);
        _panel.DOColor(_panel.color + new Color(0, 0, 0, 1), 0.25f);

        yield return new WaitForSeconds(0.15f);

        _arrow.color += new Color(0, 0, 0, 1);

        _seeds.SetActive(true);
        _seedsTxt.text = (pMaxSeeds - pSeeds) + " / " + pMaxSeeds + " ?";

        _resumeButton.gameObject.SetActive(true);
    }

    public IEnumerator ShowCheese(int pCheeses, int pMaxCheeses)
    {
        yield return new WaitForSeconds(0.15f);

        _panel.color *= new Color(1, 1, 1, 0);
        _panel.DOColor(_panel.color + new Color(0, 0, 0, 1), 0.25f);

        yield return new WaitForSeconds(0.15f);

        _arrow.color += new Color(0, 0, 0, 1);

        _cheeses.SetActive(true);
        _cheesesTxt.text = (pMaxCheeses - pCheeses) + " / " + pMaxCheeses + " ?";

        _resumeButton.gameObject.SetActive(true);
    }

    public IEnumerator ShowAdds(int pAddss, int pMaxAddss)
    {
        yield return new WaitForSeconds(0.15f);

        _panel.color *= new Color(1, 1, 1, 0);
        _panel.DOColor(_panel.color + new Color(0, 0, 0, 1), 0.25f);

        yield return new WaitForSeconds(0.15f);

        _arrow.color += new Color(0, 0, 0, 1);

        _adds.SetActive(true);
        _addsTxt.text = (pMaxAddss - pAddss) + " / " + pMaxAddss + " ?";

        _resumeButton.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        _resumeButton.gameObject.SetActive(false);
        _seeds.SetActive(false);
        _cheeses.SetActive(false);
        _adds.SetActive(false);
        _panel.color -= new Color(0, 0, 0, 1);
        _arrow.color -= new Color(0, 0, 0, 1);

        InputManager.Instance.UnlockInput();
    }
}
