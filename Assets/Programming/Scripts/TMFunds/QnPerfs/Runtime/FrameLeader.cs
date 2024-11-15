using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.QnPerfs.Runtime
{
    public class FrameLeader : MonoBehaviour
    {
        private static Action<FLeadedMonoBehaviour, FrameLeadChannel> _Connect;
        public static void Connect(FLeadedMonoBehaviour obj, FrameLeadChannel channel){_Connect(obj, channel);}
        private static Action<FLeadedMonoBehaviour, FrameLeadChannel> _Disconnect;
        public static void Disconnect(FLeadedMonoBehaviour obj, FrameLeadChannel channel){_Disconnect(obj, channel);}

        [Header("Frame Leader")]
        [SerializeField] private FrameLeadChannel channel = FrameLeadChannel.A;
        private List<FLeadedMonoBehaviour> childs = new List<FLeadedMonoBehaviour>();
        private int length = 0;

        private void Awake()
        {
            _Connect += OnConnect;
            _Disconnect += OnDisconnect;
        }

        private void OnDestroy()
        {
            _Connect -= OnConnect;
            _Disconnect -= OnDisconnect;
        }

        private void Update()
        {
            for(int i = 0; i < length; i++)
            {
                childs[i].OnFrame(Time.deltaTime);
            }
        }

        private void OnConnect(FLeadedMonoBehaviour obj, FrameLeadChannel channel)
        {
            childs.Add(obj);
            length++;
        }

        private void OnDisconnect(FLeadedMonoBehaviour obj, FrameLeadChannel channel)
        {
            childs.Remove(obj);
            length--;
        }
    }

    public enum FrameLeadChannel
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5,
        G = 6,
        H = 7,
        I = 8
    }
}


