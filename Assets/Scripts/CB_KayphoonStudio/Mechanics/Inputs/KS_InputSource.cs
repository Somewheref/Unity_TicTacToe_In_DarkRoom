using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace KayphoonStudio.Inputs
{
    public class KS_InputSource : MonoBehaviour
    {
        [ReadOnly] public KS_InputManager.InputType inputType;

        protected Vector2 currentInput;
        protected Vector2 previousInput;

        public bool isInputting = false;

        public EventHandler<OnTapInputEventArgs> OnTapInput;

        protected virtual void Initialize()
        {

        }

        /// <summary>
        /// This method will run every frame prior to any other update methods, and sets value for isInputting.
        /// </summary>
        protected virtual bool DetectInput()
        {
            return false;
        }

        protected virtual Vector2 UpdateInput()
        {
            return Vector2.zero;
        }

        protected virtual Vector2 ClampInput(Vector2 input)
        {
            return input;
        }

        protected virtual void OnInputReceived()
        {

        }

        protected virtual float SmoothInputX(float x)
        {
            return x;
        }

        protected virtual float SmoothInputY(float y)
        {
            return y;
        }

        public virtual Vector2 GetInput()
        {
            return currentInput;
        }

        public virtual Vector2 GetPointerPosition()
        {
            return Vector2.zero;
        }

        public virtual Vector2 GetInputSpeed()
        {
            if (!isInputting)
                return Vector2.zero;
            
            Vector2 deltaInput = currentInput - previousInput;
            return new Vector2(SmoothInputX(deltaInput.x), SmoothInputY(deltaInput.y)) / Time.unscaledDeltaTime;
        }


        private void Awake() {
            inputType = KS_InputManager.Instance.inputType;
            Initialize();
        }

        private void Update()
        {
            var thisFrame_isInputting = DetectInput();

            if (thisFrame_isInputting)
            {
                OnInputReceived();
            }
        
            Vector2 input = ClampInput(UpdateInput());

            // tap input
            if (thisFrame_isInputting && !isInputting)
            {
                OnTapInput?.Invoke(this, new OnTapInputEventArgs(input.x, input.y));
            }

            previousInput = currentInput;
            currentInput = input;
            isInputting = thisFrame_isInputting;
        }

        public class OnTapInputEventArgs : System.EventArgs
        {
            public float Left { get; private set; }
            public float Right { get; private set; }
            public float Up { get; private set; }
            public float Down { get; private set; }

            public float x => Right - Left;
            public float y => Up - Down;

            public OnTapInputEventArgs(float horizontal = 0, float vertical = 0)
            {
                Left = Mathf.Clamp01(-horizontal);
                Right = Mathf.Clamp01(horizontal);
                Up = Mathf.Clamp01(vertical);
                Down = Mathf.Clamp01(-vertical);
            }
        }
    }

}