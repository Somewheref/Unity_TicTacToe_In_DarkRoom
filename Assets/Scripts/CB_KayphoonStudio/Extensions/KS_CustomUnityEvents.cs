using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KayphoonStudio
{
    public class KS_CustomUnityEvents : MonoBehaviour
    {
        [System.Serializable]
        public class KS_UnityEventInt : UnityEvent<int> { }
        [System.Serializable]
        public class KS_UnityEventFloat : UnityEvent<float> { }
    }
}
