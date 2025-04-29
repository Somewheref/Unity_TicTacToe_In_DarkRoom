using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio.Components
{
    public class KS_RandomStartRotation : MonoBehaviour
    {
        public KS_Units.DOF_Axis axis = KS_Units.DOF_Axis.Y;
        public KS_Units.NumberType numberType = KS_Units.NumberType.Continuous;
        public KS_Classes.MinMaxRange constrainBetween = new KS_Classes.MinMaxRange(0.5f, 1.5f);

        protected Vector3 startRotation;

        private void Awake()
        {
            startRotation = transform.eulerAngles;
        }

        [ContextMenu("SetRandomSpawnDirection")]
        private void Start()
        {
            transform.eulerAngles = startRotation;
            SetRandomSpawnDirection(transform, axis, numberType);
        }

        public void SetRandomSpawnDirection(Transform objTransform, KS_Units.DOF_Axis axis, KS_Units.NumberType numType)
        {
            Vector3 randomDirection = Vector3.zero;
            switch (numType)
            {
                case KS_Units.NumberType.Continuous:
                    randomDirection = new Vector3(
                        KS_Random.Range(constrainBetween.min, constrainBetween.max),
                        KS_Random.Range(constrainBetween.min, constrainBetween.max),
                        KS_Random.Range(constrainBetween.min, constrainBetween.max)
                    );
                    randomDirection += objTransform.eulerAngles;
                    break;

                case KS_Units.NumberType.Discrete:
                    int min = (int)(constrainBetween.min / 90);
                    int max = (int)(constrainBetween.max / 90);
                    randomDirection = new Vector3(
                        KS_Random.Range(min, max) * 90,
                        KS_Random.Range(min, max) * 90,
                        KS_Random.Range(min, max) * 90
                    );
                    break;
            }
            switch (axis)
            {
                case KS_Units.DOF_Axis.None:
                    break;
                case KS_Units.DOF_Axis.X:
                    objTransform.eulerAngles = new Vector3(randomDirection.x, objTransform.eulerAngles.y, objTransform.eulerAngles.z);
                    break;
                case KS_Units.DOF_Axis.Y:
                    objTransform.eulerAngles = new Vector3(objTransform.eulerAngles.x, randomDirection.y, objTransform.eulerAngles.z);
                    break;
                case KS_Units.DOF_Axis.Z:
                    objTransform.eulerAngles = new Vector3(objTransform.eulerAngles.x, objTransform.eulerAngles.y, randomDirection.z);
                    break;
                case KS_Units.DOF_Axis.XY:
                    objTransform.eulerAngles = new Vector3(randomDirection.x, randomDirection.y, objTransform.eulerAngles.z);
                    break;
                case KS_Units.DOF_Axis.XZ:
                    objTransform.eulerAngles = new Vector3(randomDirection.x, objTransform.eulerAngles.y, randomDirection.z);
                    break;
                case KS_Units.DOF_Axis.YZ:
                    objTransform.eulerAngles = new Vector3(objTransform.eulerAngles.x, randomDirection.y, randomDirection.z);
                    break;
                case KS_Units.DOF_Axis.XYZ:
                    objTransform.eulerAngles = new Vector3(randomDirection.x, randomDirection.y, randomDirection.z);
                    break;
            }
        }
    }

}

