using System.Collections;
using System.Collections.Generic;
using KayphoonStudio;
using UnityEngine;

namespace KayphoonStudio.TMP
{
    public class KS_TMP_LinkEventReceiver : MonoBehaviour, KS_StringParameterInterface
    {
        public virtual void ReceiveString(string parameter)
        {
            switch (parameter)
            {
                // switch link here
                default:
                    KS_Logger.LogWarning("No link handler for " + parameter, this);
                    break;
            }
        }
    }
}

