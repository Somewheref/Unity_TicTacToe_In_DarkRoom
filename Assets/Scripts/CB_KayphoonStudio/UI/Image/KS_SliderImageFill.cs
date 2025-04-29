using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace KayphoonStudio.UI
{
    public class KS_SliderImageFill : MonoBehaviour
    {
        public Image fillImage;
        [Range(0, 1)]
        public float startValue = 1;

        private void Start()
        {
            SetRatio(startValue);
        }

        public void SetRatio(float val)
        {
            fillImage.fillAmount = val;
        }
    }

}