using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.QnPerfs.Batching
{
    public class SimpleBatcher : MonoBehaviour
    {
        private void Awake()
        {
            StaticBatchingUtility.Combine(gameObject);
        }
    }
}


