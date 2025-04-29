using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace KayphoonStudio.SimpleHelper
{
    public class KS_SimpleFollow : MonoBehaviour
    {

        public Transform target;
        public MovingMode movingMode = MovingMode.Transform;

        public float smoothingTime = 0f;

        [ShowIf("@movingMode == MovingMode.Transform")]
        public bool followRotation = false;

        [EnumToggleButtons]
        public FollowAxis followAxis = FollowAxis.All;

        private Rigidbody rb;
        private Vector3 offset;
        private Quaternion rotationOffset;

        private Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            if (target == null) return;

            offset = transform.position - target.position;
            rotationOffset = transform.rotation * Quaternion.Inverse(target.rotation);

            if (movingMode == MovingMode.Rigidbody)
            {
                rb = GetComponent<Rigidbody>();
            }
        }

        public void RecalibrateOffset()
        {
            offset = transform.position - target.position;
            rotationOffset = transform.rotation * Quaternion.Inverse(target.rotation);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (transform == null || target == null)
            {
                //Debug.LogWarning(name + " has no target");
                return;
            }
            else
            {
                Vector3 targetPos = new Vector3
                {
                    x = (followAxis & FollowAxis.X) == FollowAxis.X ? target.position.x + offset.x : transform.position.x,
                    y = (followAxis & FollowAxis.Y) == FollowAxis.Y ? target.position.y + offset.y : transform.position.y,
                    z = (followAxis & FollowAxis.Z) == FollowAxis.Z ? target.position.z + offset.z : transform.position.z
                };

                if (smoothingTime > 0)
                {
                    targetPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothingTime);
                }

                if (movingMode == MovingMode.Rigidbody)
                {
                    rb.MovePosition(targetPos);
                }
                else
                {
                    transform.position = targetPos;
                }

                if (followRotation)
                {
                    transform.rotation = target.rotation * rotationOffset;
                }
            }
        }


        public enum MovingMode
        {
            Transform,
            Rigidbody
        }


        [System.Flags]
        public enum FollowAxis
        {
            X = 1 << 1,
            Y = 1 << 2,
            Z = 1 << 3,
            All = X | Y | Z
        }
    }

}

