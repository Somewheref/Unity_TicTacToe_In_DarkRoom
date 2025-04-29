using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace KayphoonStudio.EditorHelpers
{
    public class KS_IgnoreParentTransform : MonoBehaviour
    {
        [System.Flags]
        [EnumToggleButtons]
        public enum IgnoreTransform
        {
            None = 0,
            Position = 1,
            Rotation = 2,
            Scale = 4,
            All = Position | Rotation | Scale
        }

        public IgnoreTransform ignoreTransform = IgnoreTransform.All;

        private Vector3 localPosition;
        private Quaternion localRotation;
        private Vector3 localScale;

        private void Awake()
        {
            localPosition = transform.localPosition;
            localRotation = transform.localRotation;
            localScale = transform.localScale;
        }

        private void LateUpdate()
        {
            if (ignoreTransform.HasFlag(IgnoreTransform.Position))
            {
                transform.localPosition = localPosition;
            }

            if (ignoreTransform.HasFlag(IgnoreTransform.Rotation))
            {
                transform.localRotation = localRotation;
            }

            if (ignoreTransform.HasFlag(IgnoreTransform.Scale))
            {
                transform.localScale = localScale;
            }
        }
    }

}