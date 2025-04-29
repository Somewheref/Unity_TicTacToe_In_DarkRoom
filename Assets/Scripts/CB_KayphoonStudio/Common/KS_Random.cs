using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio
{
    public class KS_Random : MonoBehaviour
    {
        public static Vector3 insideUnitSphere
        {
            get
            {
                return UnityEngine.Random.insideUnitSphere;
            }
        }
        public static float Range(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static int Range(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public static float Range(Vector2 range)
        {
            return UnityEngine.Random.Range(range.x, range.y);
        }

        public static T RandomFromList<T>(List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
    }

}
