using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMFunds.Audio
{
    public class AudioUIConnector : MonoBehaviour
    {
        private void Awake()
        {
            AudioPlayer audio = GetComponent<AudioPlayer>();
            UI.UIEventEmitter ui = GetComponent<UI.UIEventEmitter>();

            if (audio != null && ui != null) ui.OnPlay += audio.Play;

            Destroy(this);
        }
    }

}

