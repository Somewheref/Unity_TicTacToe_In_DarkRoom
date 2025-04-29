using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_Units : MonoBehaviour
    {
        /// <summary>
        /// Single axis of a 3D vector, include X, Y, Z, -X, -Y, -Z. Use AxisToVector() to get the vector
        /// </summary>
        public enum SingleAxis
        {
            None,
            X,
            Y,
            Z,
            [InspectorName("-X")]
            MinusX,
            [InspectorName("-Y")]
            MinusY,
            [InspectorName("-Z")]
            MinusZ
        }

        /// <summary>
        /// Multiple axis of a 3D vector, include X, Y, Z, XY, XZ, YZ, XYZ. Use AxisToVector() to get the vector
        /// </summary>
        [System.Flags]
        public enum DOF_Axis
        {
            None = 0,
            X = 1,
            Y = 2,
            Z = 4,
            XY = X | Y,
            XZ = X | Z,
            YZ = Y | Z,
            XYZ = X | Y | Z
        }

        public enum UpdateType
        {
            Update,
            FixedUpdate,
            LateUpdate
        }

        public enum NumberType
        {
            Continuous,
            Discrete
        }


        public static Vector3 AxisToVector(SingleAxis axis)
        {
            switch (axis)
            {
                case SingleAxis.X:
                    return Vector3.right;
                case SingleAxis.Y:
                    return Vector3.up;
                case SingleAxis.Z:
                    return Vector3.forward;
                case SingleAxis.MinusX:
                    return Vector3.left;
                case SingleAxis.MinusY:
                    return Vector3.down;
                case SingleAxis.MinusZ:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
        }


        public static Vector3 AxisToVector(DOF_Axis axis)
        {
            switch (axis)
            {
                case DOF_Axis.X:
                    return Vector3.right;
                case DOF_Axis.Y:
                    return Vector3.up;
                case DOF_Axis.Z:
                    return Vector3.forward;
                case DOF_Axis.XY:
                    return new Vector3(1, 1, 0);
                case DOF_Axis.XZ:
                    return new Vector3(1, 0, 1);
                case DOF_Axis.YZ:
                    return new Vector3(0, 1, 1);
                case DOF_Axis.XYZ:
                    return Vector3.one;
                default:
                    return Vector3.zero;
            }
        }
    
    }
}
