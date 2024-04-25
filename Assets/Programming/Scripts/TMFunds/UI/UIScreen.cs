using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.UI
{
    public class UIScreen : UIEventEmitter
    {
        private static List<UIScreen> list = new List<UIScreen>();

        [Header("Screen")]
        [SerializeField] private ScreenChannel channel;
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorPrompt;
        public ScreenChannel Channel => channel;

        [Header("Panel")]
        [SerializeField] protected List<UIButton> buttons;
        public List<UIButton> buttonList { get => buttons; }

        new protected void Awake()
        {
            base.Awake();
            list.Add(this);
        }

        protected void OnDestroy()
        {
            list.Remove(this);
        }

        public void Close()
        {
            StopAllCoroutines();
            OnClose();
            animator.SetBool(animatorPrompt, false);
            StartCoroutine(DestroyAfter(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
            for(int i = buttons.Count - 1; i >= 0; i--)
            {
                buttons[i].clickable = false;
            }
        }

        private IEnumerator DestroyAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            OnOff();
            gameObject.SetActive(false);
        }

        //Called on screen closing
        virtual protected void OnClose() { }

        public void Open()
        {
            StopAllCoroutines();
            gameObject.SetActive(true);
            OnOpen();
            OnPlay();
            animator.SetBool(animatorPrompt, true);
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                buttons[i].clickable = true;
            }
        }

        //Called on screen opening
        virtual protected void OnOpen() { }

        //Open screen on specified channel
        public static void Open(ScreenChannel channel)
        {
            for(int i = list.Count - 1; i >= 0; i--)
            {
                if(list[i].channel == channel)
                {
                    list[i].Open();
                }
            }
        }

        //Close screen on specified channel
        public static void Close(ScreenChannel channel)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].channel == channel)
                {
                    list[i].Close();
                }
            }
        }
    }

    public enum ScreenChannel
    {
        SPLASH = 0,
        MAIN0 = 1,
        MAIN1 = 2,
        MAIN2 = 3,
        OPTION0 = 4,
        OPTION1 = 5,
        OPTION2 = 6,
        HUD0 = 7,
        HUD1 = 8,
        HUD2 = 9,
        INVENTORY0 = 10,
        INVENTORY1 = 11,
        INVENTORY2 = 12,
        LEVELS0 = 13,
        LEVELS1 = 14,
        LEVELS2 = 15,
        GAME0 = 16,
        GAME1 = 17,
        GAME2 = 18,
        CREDIT = 19,
        WIN = 20,
        LOSE = 21,
        OTHER0 = 22,
        OTHER1 = 23,
        OTHER2 = 24,
        OTHER3 = 25,
        OTHER4 = 26,
        OTHER5 = 27,
        OTHER6 = 28,
        OTHER7 = 29,
        OTHER8 = 30,
        OTHER9 = 31,
        LOGIN = 32,
        LEADERBOARD = 33,
        PAUSE = 34,
        LEVELSELECTOR = 35,
        TITLESCREEN = 36
    }

}

