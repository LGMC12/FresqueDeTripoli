using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMFunds.UI;

public class UIScreenAnimationEvent : MonoBehaviour
{
    public virtual void OnAnimationDone() { }

    public virtual void OnOpenAnimationDone() { }

    public virtual void OnCloseAnimationDone() { }
}