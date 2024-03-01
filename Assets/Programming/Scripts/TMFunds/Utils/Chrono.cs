using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TMFunds
{
    public class Chrono : MonoBehaviour
    {
        public float CurrentTime { get { return count; } }
        private float count = 0;
        private Action action;

        private void Awake()
        {
            action = VoidAction;
        }

        private void Update()
        {
            action();
        }

        private void PlayingAction()
        {
            count += Time.deltaTime;
        }

        private void VoidAction() { }

        public void Play()
        {
            action = PlayingAction;
        }

        public void Pause()
        {
            action = VoidAction;
        }

        public void Stop()
        {
            Pause();
            count = 0;
        }

        public static Chrono Init(GameObject parent)
        {
            return parent.AddComponent<Chrono>();
        }
    }
}


