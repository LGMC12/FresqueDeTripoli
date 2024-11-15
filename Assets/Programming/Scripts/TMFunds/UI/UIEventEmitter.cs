using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.UI
{
    public abstract class UIEventEmitter : MonoBehaviour
    {
        public Action OnPlay;

        public Action OnOff;

        protected void Awake()
        {
            OnPlay += VoidEvent;
            OnOff += VoidEvent;
        }

        private void VoidEvent() { }
    }
}


