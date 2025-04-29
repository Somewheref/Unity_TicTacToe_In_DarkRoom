using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace KayphoonStudio.EditorHelpers
{
    public class KS_TriggerEnter2D : MonoBehaviour
    {
        public UnityEvent onTriggerEnter;
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

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!CheckTag(other.tag)) return;

            onTriggerEnter?.Invoke();
        }
    }
}
