using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KayphoonStudio.Inputs
{
    public class KS_InputManager : KS_Singleton<KS_InputManager>
    {
        public enum InputType
        {
            Keyboard,
            Mouse,
            Touch
        }

        public InputType inputType = InputType.Mouse;
        public bool autoDetect;

        [ReadOnly] public KS_InputSource inputSource;

        protected virtual void Awake()
        {
            AutoDetectInput();

            switch (inputType)
            {
                case InputType.Keyboard:
                    inputSource = gameObject.AddComponent<KS_KeyboardInput>();
                    break;
                case InputType.Mouse:
                    inputSource = gameObject.AddComponent<KS_MouseInput>();
                    break;
                case InputType.Touch:
                    inputSource = gameObject.AddComponent<KS_TouchInput>();
                    break;
                default:
                    break;
            }
        }

        [ContextMenu("AutoDetectInput")]
        public virtual void AutoDetectInput()
        {
            if (autoDetect)
            {
                #if UNITY_EDITOR
                inputType = InputType.Keyboard;
                #elif UNITY_ANDROID || UNITY_IOS
                inputType = InputType.Touch;
                #endif
            }
        }
    }

}
