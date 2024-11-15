using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }
}
