using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    public static LoadingScreen Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _canvas.SetActive(false);
    }
    
    public void ShowLoadingScreen() { _canvas.SetActive(true); }
}
