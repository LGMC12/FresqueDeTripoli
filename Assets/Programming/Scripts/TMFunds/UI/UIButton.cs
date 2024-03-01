using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TMFunds.UI
{
    public class UIButton : UIEventEmitter, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Button")]
        [SerializeField] public bool clickable = true;
        [SerializeField] private bool keepDown = false;
        [HideInInspector] public bool selected = false;

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            OnTriggerIn();
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            OnTriggerOut();
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (clickable)
            {
                OnRelease();
                if (selected && !keepDown)
                {
                    selected = false;
                    OnDeselect();
                }
            }
        }

        public void Click()
        {
            OnPointerDown(null);
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            if (clickable)
            {
                OnPlay?.Invoke();
                OnClick();
                if (!selected)
                {
                    selected = true;
                    OnSelect();
                }
                
            }
        }

        protected virtual void OnClick(){}

        protected virtual void OnRelease(){}

        protected virtual void OnSelect(){}

        protected virtual void OnDeselect(){}

        protected virtual void OnTriggerIn(){}

        protected virtual void OnTriggerOut(){}

    }
}

