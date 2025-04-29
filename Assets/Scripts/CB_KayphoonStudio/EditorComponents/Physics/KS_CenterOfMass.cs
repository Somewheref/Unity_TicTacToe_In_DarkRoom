using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.EditorHelpers
{
    public class KS_CenterOfMass : MonoBehaviour
    {
        public Vector3 centerOfMass = Vector3.zero;
        public Rigidbody rb;

        private void Reset() {
            if (rb == null)
                rb = GetComponent<Rigidbody>();
            centerOfMass = rb.centerOfMass;
        }

        void Start()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
                rb.centerOfMass = centerOfMass;
            }
                
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + centerOfMass, 0.1f);
        }
    }

}
