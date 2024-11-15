using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Patterns
{
    public class StateMachine
    {
        private Action func;

        public StateMachine()
        {
            SetVoid();
        }

        private void VoidCall() { }

        //Call the state function of the class
        public void Call()
        {
            func();
        }

        //Set the current function of the class
        public void Set(Action call)
        {
            func = call;
        }

        //Set the current function of the class
        public void Set(Action call, Action call2)
        {
            func = call;
            func += call2;
        }

        //Set the current function of the class
        public void Set(Action call, Action call2, Action call3)
        {
            func = call;
            func += call2;
            func += call3;
        }

        public void Add(Action call)
		{
            func += call;
		}

        //Set multiples current states
        public void Set(Action[] calls)
        {
            func = null;
            for(int i = calls.Length - 1; i >= 0; i--)
            {
                func += calls[i];
            }
        }

        //Unactive the function of the class
        public void SetVoid()
        {
            func = VoidCall;
        }

        public bool IsVoid() => func == VoidCall;
    }
}


