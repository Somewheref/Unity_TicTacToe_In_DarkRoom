using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio.Inputs
{
    public class KS_KeyboardInput : KS_InputSource
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override bool DetectInput()
        {
            return UpdateInput() != Vector2.zero;
        }

        public override Vector2 GetPointerPosition()
        {
            return Input.mousePosition;
        }

        protected override Vector2 UpdateInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

}