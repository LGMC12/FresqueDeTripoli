using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimEvent : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    
    public void AnimFinished()
    {
        StartCoroutine(RespawnCollectibles());
    }

    private IEnumerator RespawnCollectibles()
    {
        foreach (Image PImage in images)
        {
            PImage.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
        
        StopAllCoroutines();
    }
}
