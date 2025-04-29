using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

using Sirenix.OdinInspector;

namespace KayphoonStudio.EditorHelpers
{
    public class KS_OnTriggerEvent : MonoBehaviour
    {
        public bool filterTag = false;
        [ShowIf("filterTag")]
        public string[] filter_allowedTags;
        
        public UnityEvent onTriggerEnter;
        public UnityEvent onTriggerExit;

        // custom event that passes the collider that triggered the event
        public event Action<Collider> onTriggerEnterCustom;
        public event Action<Collider> onTriggerExitCustom;

        protected Collider _triggerCollider;
        public Collider triggerCollider
        {
            get
            {
                if (_triggerCollider == null)
                {
                    _triggerCollider = GetComponent<Collider>();
                }
                return _triggerCollider;
            }
            set
            {
                _triggerCollider = value;
            }
        }

        private void Awake()
        {
            triggerCollider = GetComponent<Collider>();
        }

        protected virtual void OnTriggerEnter(Collider other) 
        {
            if (!CheckTag(other.tag)) return;

            onTriggerEnter?.Invoke();
            onTriggerEnterCustom?.Invoke(other);
        }

        protected virtual void OnTriggerExit(Collider other) 
        {
            if (!CheckTag(other.tag)) return;

            onTriggerExit?.Invoke();
            onTriggerExitCustom?.Invoke(other);
        }

        protected bool CheckTag(string tag)
        {
            if (filterTag)
            {
                for (int i = 0; i < filter_allowedTags.Length; i++)
                {
                    if (tag == filter_allowedTags[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}