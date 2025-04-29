using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.UI
{
    public class KS_WorldCanvasBillboard : MonoBehaviour
    {
        public Camera cam;

        private void Reset() {
            cam = Camera.main;
        }

        private void Start()
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        }
    }

}
