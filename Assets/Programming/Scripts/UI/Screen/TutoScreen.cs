using System;
using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;
using UnityEngine.UI;

public class TutoScreen : ScreenUI
{
    public static Action OnTutoFinished;

    [SerializeField] private List<GameObject> _panels = new List<GameObject>();
    
    private int _currentPanelIndex = 0;
    
    [SerializeField] private AnimatedButton _nextButton;

    private void Awake()
    {
        _nextButton.OnPlay += NextPanel;
        TutoEvent.OnOpenAnimDone += OpenAnimDone;
    }

    public void OpenAnimDone()
    {
        _panels[_currentPanelIndex].SetActive(true);
    }

    private void NextPanel()
    {
        _panels[_currentPanelIndex].SetActive(false);
        
        ++_currentPanelIndex;
        
        if(_currentPanelIndex < _panels.Count) _panels[_currentPanelIndex].SetActive(true);
        else OnTutoFinished?.Invoke();
    }

    protected override void OnDestroy()
    {
        _nextButton.OnPlay -= NextPanel;
        TutoEvent.OnOpenAnimDone -= OpenAnimDone;
    }
}
