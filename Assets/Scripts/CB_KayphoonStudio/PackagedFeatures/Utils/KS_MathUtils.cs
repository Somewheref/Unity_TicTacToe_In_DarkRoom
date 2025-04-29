using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_MathUtils : MonoBehaviour
    {
        public static float ClampAngle180(float angle)
        {
            if (angle > 180)
                angle -= 360;
            else if (angle < -180)
                angle += 360;

            return angle;
        }

        public static float ClampAngle360(float angle)
        {
            if (angle > 360)
                angle -= 360;
            else if (angle < 0)
                angle += 360;

            return angle;
        }
    }

}