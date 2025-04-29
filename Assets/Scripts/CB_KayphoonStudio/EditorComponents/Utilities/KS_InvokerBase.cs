using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using KayphoonStudio;

namespace KayphoonStudio.BaseClass
{

    public class KS_InvokerBase : MonoBehaviour
    {
        public DelayType delayType = DelayType.None;
        [ShowIf("@delayType == DelayType.Fixed")]
        public float StartDelay = 0;

        [ShowIf("@delayType == DelayType.Random")]
        public float MinDelay = 0;
        [ShowIf("@delayType == DelayType.Random")]
        public float MaxDelay = 1;

        protected float _actualStartDelay
        {
            get
            {
                switch (delayType)
                {
                    case DelayType.Fixed:
                        return StartDelay;
                    case DelayType.Random:
                        return KS_Random.Range(MinDelay, MaxDelay);
                    default:
                        return 0;
                }
            }
        }

        public enum DelayType
        {
            None,
            Random,
            Fixed
        }
    }

}
