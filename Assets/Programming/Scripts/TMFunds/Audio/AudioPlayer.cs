using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TMFunds.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public enum PlayMethod
        {
            RANDOM = 0,
            TO_NEXT = 1,
            SINGLE = 2
        }

        [Header("Player")]
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private PlayMethod method = PlayMethod.RANDOM;
        [SerializeField] private AudioMixerGroup layer;
        [Range(0f, 1f)] [SerializeField] private float volume = 1f;
        [Range(0f, 2f)] [SerializeField] private float pitch = 1f;
        //[SerializeField] private bool connectToUI = true;
        //private UI.UIEventEmitter uiConnector = null;
        protected AudioSource source;

        protected int cursor = 0;

        private Action GetClip;

        protected void Awake()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.volume = volume;
            source.pitch = pitch;
            source.outputAudioMixerGroup = layer;
            source.loop = false;
            source.playOnAwake = false;

            //if (connectToUI && GetComponent<UI.UIEventEmitter>())
            //{
            //    uiConnector = GetComponent<UI.UIEventEmitter>();
            //    uiConnector.OnPlay += Play;
            //}

            if(method == PlayMethod.RANDOM)
            {
                GetClip = GetRandom;
            }else if(method == PlayMethod.TO_NEXT)
            {
                GetClip = GetNext;
            }
            else
            {
                GetClip = GetSingle;
            }
        }

        virtual public void Play()
        {
            GetClip();
            source.Play();
        }

        public AudioSource GetSource()
        {
            return source;
        }

        private void GetRandom()
        {
            source.clip = clips[Mathf.FloorToInt(UnityEngine.Random.Range(0, clips.Length))];
        }

        private void GetNext()
        {
            source.clip = clips[cursor];
            cursor = (cursor + 1) % clips.Length;
        }

        private void GetSingle()
        {
            source.clip = clips[0];
        }

        /*
        private void OnDestroy()
        {
            if(uiConnector != null)
            {
                uiConnector.OnPlay -= Play;
            }
        }
        */

    }

}

