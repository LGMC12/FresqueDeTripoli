using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : MonoBehaviour
{
    [SerializeField] private GameObject _compositionContainer;
    public static List<GameObject> _compositionList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _compositionContainer.transform.childCount; i++)
        {
            _compositionList.Add(_compositionContainer.transform.GetChild(i).gameObject);
        }

        CompositionSelector.OnCompositionSelected += ShowComposition;
    }

    public static void ShowComposition(int pIndex)
    {
        _compositionList[pIndex].SetActive(true);
    }

    public static void HideComposition(int pIndex)
    {
        _compositionList[pIndex].SetActive(false);
    }

    public static void HideAll()
    {
        foreach (GameObject item in _compositionList)
        {
            item.SetActive(false);
        }
    }
}