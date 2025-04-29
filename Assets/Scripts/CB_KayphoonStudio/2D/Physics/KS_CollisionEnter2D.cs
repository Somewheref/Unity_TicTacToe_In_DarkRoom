using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace KayphoonStudio._2D.Physics
{
    public class KS_CollisionEnter2D : MonoBehaviour
    {
        public UnityEvent onCollisionEnter;
        public bool filterTag = false;
        [ShowIf("filterTag")]
        public string[] filter_allowedTags;

        private bool CheckTag(string tag)
        {
            if (!filterTag) return true;

            foreach (var allowedTag in filter_allowedTags)
            {
                if (tag == allowedTag)
                {
                    return true;
                }
            }

            return false;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!CheckTag(other.collider.tag)) return;

            onCollisionEnter?.Invoke();
        }
    }
}