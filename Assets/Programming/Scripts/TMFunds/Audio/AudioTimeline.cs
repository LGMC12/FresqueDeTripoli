using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Audio
{
    public class AudioTimeline : AudioPlayer
    {
        [Header("TimeLine")]
        [SerializeField] private float[] delays;

        public override void Play()
        {
            StartCoroutine(_Play());
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        private IEnumerator _Play()
        {
            cursor = 0;
            int lLength = delays.Length;

            for(int i = 0; i < lLength; i++)
            {
                yield return new WaitForSeconds(delays[i]);
                base.Play();
            }
        }
    }

}

