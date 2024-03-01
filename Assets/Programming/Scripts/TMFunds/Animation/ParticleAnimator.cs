using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Animation
{
    public class ParticleAnimator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;

        public void Play(int pIndex)
        {
            particles[pIndex].Play();
        }

        public void PlayDuring(int pIndex, float pTime)
        {
            particles[pIndex].startLifetime = pTime;
            particles[pIndex].Play();
        }
    }
}


