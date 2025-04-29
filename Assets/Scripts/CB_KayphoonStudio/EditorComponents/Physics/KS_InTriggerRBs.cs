using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.EditorHelpers
{
    [RequireComponent(typeof(KS_OnTriggerEvent))]
    public class KS_InTriggerRBs : MonoBehaviour
    {
        public KS_OnTriggerEvent onTriggerEvent;

        [HideInInspector] public List<Rigidbody> rigidbodiesInTrigger = new List<Rigidbody>();

        private void Reset() {
            onTriggerEvent = GetComponent<KS_OnTriggerEvent>();
        }

        private void OnEnable() {
            onTriggerEvent.onTriggerEnterCustom += OnTriggerEnterCustom;
            onTriggerEvent.onTriggerExitCustom += OnTriggerExitCustom;
        }

        private void OnDisable() {
            onTriggerEvent.onTriggerEnterCustom -= OnTriggerEnterCustom;
            onTriggerEvent.onTriggerExitCustom -= OnTriggerExitCustom;
        }

        private void OnTriggerEnterCustom(Collider other) {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            if (otherRB != null && !rigidbodiesInTrigger.Contains(otherRB)) {
                rigidbodiesInTrigger.Add(otherRB);
            }
        }

        private void OnTriggerExitCustom(Collider other) {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            if (otherRB != null && rigidbodiesInTrigger.Contains(otherRB)) {
                rigidbodiesInTrigger.Remove(otherRB);
            }
        }
    }

}