using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class CompositionScreen : ScreenUI
{
    public static Action OnBack;

    [SerializeField] private string _compo1Url;
    [SerializeField] private string _compo2Url;
    [SerializeField] private string _compo3Url;
    [SerializeField] private string _compo4Url;
    [SerializeField] private string _compo5Url;
    [SerializeField] private string _compo6Url;

    [SerializeField] private AnimatedButton _back;
    [SerializeField] private AnimatedButton _compo1;
    [SerializeField] private AnimatedButton _compo2;
    [SerializeField] private AnimatedButton _compo3;
    [SerializeField] private AnimatedButton _compo4;
    [SerializeField] private AnimatedButton _compo6;
    [SerializeField] private AnimatedButton _compo5;

    protected override void Start()
    {
        _back.OnPlay += Back;
        _compo1.OnPlay += Compo1;
        _compo2.OnPlay += Compo2;
        _compo3.OnPlay += Compo3;
        _compo4.OnPlay += Compo4;
        _compo5.OnPlay += Compo5;
        _compo6.OnPlay += Compo6;
    }

    private void Back() { OnBack?.Invoke(); }
    private void Compo1()
    {
        Application.OpenURL(_compo1Url);
    }

    private void Compo2()
    {
        Application.OpenURL(_compo2Url);
    }

    private void Compo3()
    {
        Application.OpenURL(_compo3Url);
    }

    private void Compo4()
    {
        Application.OpenURL(_compo4Url);
    }

    private void Compo5()
    {
        Application.OpenURL(_compo5Url);
    }

    private void Compo6()
    {
        Application.OpenURL(_compo6Url);
    }

    protected override void OnDestroy()
    {
        _back.OnPlay -= Back;
        _compo1.OnPlay -= Compo1;
        _compo2.OnPlay -= Compo2;
        _compo3.OnPlay -= Compo3;
        _compo4.OnPlay -= Compo4;
        _compo5.OnPlay -= Compo5;
        _compo6.OnPlay -= Compo6;
    }
}
