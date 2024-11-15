using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.UI
{
    public class AnimatedButton : UIButton
    {

        [Header("Juiciness")]
        [SerializeField] protected Animator animator;
        [SerializeField] private string buttonShowPrompt = "show";
        [SerializeField] private string clickPrompt = "click";

        [SerializeField] private AudioSource _sfx;
        protected override void OnTriggerIn()
        {
            animator.SetBool(buttonShowPrompt, true);
        }

        protected override void OnTriggerOut()
        {
            animator.SetBool(buttonShowPrompt, false);
        }

        protected override void Clicking()
        {
            _sfx.Play();
            animator.SetTrigger(clickPrompt);
            StartCoroutine(DestroyAfter(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
        }

        private IEnumerator DestroyAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            OnOff();
        }
    }
}


