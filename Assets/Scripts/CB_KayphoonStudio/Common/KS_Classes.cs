using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio
{
    public class KS_Classes
    {
        [System.Serializable]
        public class MinMaxRange
        {
            public float min;
            public float max;

            public MinMaxRange(float min, float max)
            {
                this.min = min;
                this.max = max;
            }

            public float RandomValue()
            {
                return KS_Random.Range(min, max);
            }
        }
    }
}