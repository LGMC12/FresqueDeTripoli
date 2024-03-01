using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.QnPerfs.Runtime
{
    public class FLeadedMonoBehaviour : MonoBehaviour
    {
        [Header("Frame Follower")]
        [SerializeField] private FrameLeadChannel channel = FrameLeadChannel.A;

        virtual public void OnFrame(float delta) { }

        virtual protected void BeforeDestroy(){}

        virtual protected void AfterAwake() { }

        private void Start()
        {
            FrameLeader.Connect(this, channel);
            AfterAwake();
        }

        private void OnDestroy()
        {
            BeforeDestroy();
            FrameLeader.Disconnect(this, channel);
        }
    }
}


