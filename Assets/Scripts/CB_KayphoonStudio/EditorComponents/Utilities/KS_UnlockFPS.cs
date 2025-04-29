using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.Utilities
{
    public class KS_UnlockFPS : MonoBehaviour
    {
        public int targetFPS = 60;

        private void Awake() 
        {
            Application.targetFrameRate = targetFPS;
        }
    }

}

