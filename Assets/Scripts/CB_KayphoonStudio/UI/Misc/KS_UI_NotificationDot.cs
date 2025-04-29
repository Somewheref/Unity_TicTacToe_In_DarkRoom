using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace KayphoonStudio.UI
{
    public class KS_UI_NotificationDot : MonoBehaviour
    {
        public Image dotImage;

        private void Reset()
        {
            dotImage = GetComponent<Image>();
        }

        protected virtual void Start()
        {
            UpdateDot();
        }

        public virtual void SetDot(bool value)
        {
            dotImage.enabled = value;
        }

        protected virtual bool CheckCondition()
        {
            return false;
        }

        protected virtual void UpdateDot()
        {
            SetDot(CheckCondition());
        }

    }

}
