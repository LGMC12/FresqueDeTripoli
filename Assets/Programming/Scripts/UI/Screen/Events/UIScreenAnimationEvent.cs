using System.Collections;
using System.Collections.Generic;
using TMFunds.UI;
using UnityEngine;

public class UIScreenAnimationEvent : MonoBehaviour
{
    public virtual void OnAnimationDone() { }

    public virtual void OnOpenAnimationDone() { }

    public virtual void OnCloseAnimationDone() { }
}