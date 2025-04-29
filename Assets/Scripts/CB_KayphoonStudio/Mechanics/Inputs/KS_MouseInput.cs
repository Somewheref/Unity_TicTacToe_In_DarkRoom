using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.Inputs
{
    public class KS_MouseInput : KS_InputSource
    {
        protected Camera mainCamera;

        protected override void Initialize()
        {
            base.Initialize();
            mainCamera = Camera.main;
        }

        protected override bool DetectInput()
        {
            return Input.GetMouseButton(0);
        }

        protected override Vector2 UpdateInput()
        {
            // mouse position in world space
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            return mousePos;
        }
    }

}