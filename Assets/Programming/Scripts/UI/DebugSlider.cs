using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSlider : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private TMPro.TextMeshProUGUI _txt;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        UpdateVal();
    }

    public void UpdateVal()
    {
        _txt.text = _slider.value.ToString();
    }
}