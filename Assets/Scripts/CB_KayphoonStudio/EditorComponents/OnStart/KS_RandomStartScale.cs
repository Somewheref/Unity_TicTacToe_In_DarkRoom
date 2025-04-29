using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio.EditorComponents
{
    public class KS_RandomStartScale : MonoBehaviour
    {
        public KS_Units.DOF_Axis axis = KS_Units.DOF_Axis.Y;
        public KS_Classes.MinMaxRange minMaxRange = new KS_Classes.MinMaxRange(0.5f, 1.5f);
        public bool uniformScale = false;

        protected Vector3 startScale;

        private void Awake()
        {
            startScale = transform.localScale;
        }

        [ContextMenu("SetRandomSpawnScale")]
        private void Start()
        {
            transform.localScale = startScale;
            SetRandomSpawnScale(transform, axis, minMaxRange);
        }

        public void SetRandomSpawnScale(Transform objTransform, KS_Units.DOF_Axis axis, KS_Classes.MinMaxRange minMaxRange)
        {
            Vector3 axisVector = KS_Units.AxisToVector(axis);

            if (uniformScale)
            {
                float randomScale = KS_Random.Range(minMaxRange.min, minMaxRange.max) * objTransform.localScale.x;
                objTransform.localScale = new Vector3(randomScale, randomScale, randomScale);
                return;
            }
            else
            {
                Vector3 randomScale = new Vector3(
                    axisVector.x == 0 ? objTransform.localScale.x : KS_Random.Range(minMaxRange.min, minMaxRange.max) * objTransform.localScale.x,
                    axisVector.y == 0 ? objTransform.localScale.y : KS_Random.Range(minMaxRange.min, minMaxRange.max) * objTransform.localScale.y,
                    axisVector.z == 0 ? objTransform.localScale.z : KS_Random.Range(minMaxRange.min, minMaxRange.max) * objTransform.localScale.z
                );

                objTransform.localScale = randomScale;
            }
        }
    }
}