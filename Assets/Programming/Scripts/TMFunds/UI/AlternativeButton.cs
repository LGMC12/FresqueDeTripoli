using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TMFunds.UI
{
    public class AlternativeButton : AnimatedButton
    {
        [Header("Alternative")]
        [SerializeField] private KeyCode input;
        private Action checkBehaviour;

        new protected void Awake()
        {
            base.Awake();
            checkBehaviour = VoidCheck;
        }

        private void Update()
        {
            checkBehaviour();
        }

        public void SetAlternative(bool pValue)
        {
            checkBehaviour = pValue ? Check : VoidCheck;
        }

        private void VoidCheck()
        {

        }

        private void Check()
        {
            if (Input.GetKeyDown(input)) Click();
        }

    }
}


