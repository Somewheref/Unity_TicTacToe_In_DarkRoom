using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.Inputs
{
    public class KS_TouchInput : KS_InputSource
    {
        protected Camera mainCamera;

        protected override void Initialize()
        {
            mainCamera = Camera.main;
            base.Initialize();
        }

        protected override bool DetectInput()
        {
            return UpdateInput() != Vector2.zero;
        }

        public override Vector2 GetPointerPosition()
        {
            if (Input.touchCount == 0)
            {
                return Vector2.zero;
            }
            return Input.GetTouch(0).position;
        }

        protected override Vector2 UpdateInput()
        {
            // return touch position in world space
            return mainCamera.ScreenToWorldPoint(new Vector2(Input.GetTouch(0).position.x, 0));
        }
    }

}