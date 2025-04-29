using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace KayphoonStudio.EditorComponents
{
    public class KS_RandomStartPosition : MonoBehaviour
    {
        public KS_Units.DOF_Axis axis;

        protected Vector3 axisVector
        {
            get { return KS_Units.AxisToVector(axis); }
        }

        [ShowIf("@axisVector.x != 0")]
        public KS_Classes.MinMaxRange RangeX = new KS_Classes.MinMaxRange(-1, 1);
        [ShowIf("@axisVector.y != 0")]
        public KS_Classes.MinMaxRange RangeY = new KS_Classes.MinMaxRange(-1, 1);
        [ShowIf("@axisVector.z != 0")]
        public KS_Classes.MinMaxRange RangeZ = new KS_Classes.MinMaxRange(-1, 1);

        public bool randomizeOnStart = true;

        protected Vector3 startPosition;

        protected virtual void Awake()
        {
            startPosition = transform.localPosition;
        }

        protected virtual void Start()
        {
            if (randomizeOnStart)
            {
                transform.localPosition = startPosition;
                Randomize();
            }
        }

        [ContextMenu("Randomize")]
        public virtual void Randomize()
        {
            if (axisVector.x != 0)
                transform.localPosition += Vector3.right * Random.Range(RangeX.min, RangeX.max);
            if (axisVector.y != 0)
                transform.localPosition += Vector3.up * Random.Range(RangeY.min, RangeY.max);
            if (axisVector.z != 0)
                transform.localPosition += Vector3.forward * Random.Range(RangeZ.min, RangeZ.max);
        }


        private void OnDrawGizmosSelected() {
            Vector3 BoxSize = new Vector3
            (
                axisVector.x != 0 ? RangeX.max - RangeX.min : 1,
                axisVector.y != 0 ? RangeY.max - RangeY.min : 1,
                axisVector.z != 0 ? RangeZ.max - RangeZ.min : 1
            );

            Vector3 BoxCenter = new Vector3
            (
                axisVector.x != 0 ? (RangeX.max + RangeX.min) / 2 : 0,
                axisVector.y != 0 ? (RangeY.max + RangeY.min) / 2 : 0,
                axisVector.z != 0 ? (RangeZ.max + RangeZ.min) / 2 : 0
            );

            Gizmos.color = KS_Color.Transparent_Cyan;
            Gizmos.DrawCube(transform.position + BoxCenter, BoxSize);
        }
    }
}