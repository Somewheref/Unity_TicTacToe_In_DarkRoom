using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace KayphoonStudio.UI
{
    /// <summary>
    /// Custom button class that supports on click and on release events.
    /// </summary>
    public class KS_CustomButton : Button
    {
        public delegate void OnClickDelegate();
        public delegate void OnHoldDelegate();
        public delegate void OnReleaseDelegate();

        public event OnClickDelegate OnClick;
        public event OnHoldDelegate OnHold;
        public event OnReleaseDelegate OnRelease;

        [HideInInspector] public bool _isHolding = false;

        protected override void Start()
        {
            base.Start();
        }

        private void OnClickEvent()
        {
            OnClick?.Invoke();
        }

        private void Update()
        {
            if (IsPressed())
            {
                if (!_isHolding)
                {
                    _isHolding = true;
                    OnClickEvent();
                }

                OnHold?.Invoke();
            }
            else
            {
                if (_isHolding)
                {
                    _isHolding = false;
                    OnRelease?.Invoke();
                }
            }
        }

        public void PressEffect(bool isPressed)
        {
            if (colors == null || image == null)
                return;
            
            if (isPressed)
            {
                // make the button look pressed
                image.color = colors.pressedColor;
            }

            else
            {
                // make the button look normal
                image.color = colors.normalColor;
            }
        }
    }

}