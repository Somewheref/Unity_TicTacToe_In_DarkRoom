using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KayphoonStudio.UI
{
    public class KS_SliderNumberDisplay : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI text;
        public DisplayFormat displayFormat = DisplayFormat.Normal;

        public enum DisplayFormat
        {
            Normal,
            Percentage
        }

        private void Reset() {
            slider = GetComponent<Slider>();
        }

        private void Start()
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(slider.value);
        }

        private void OnSliderValueChanged(float val)
        {
            switch(displayFormat)
            {
                case DisplayFormat.Normal:
                    text.text = val.ToString();
                    break;
                case DisplayFormat.Percentage:
                    text.text = (val * 100).ToString("0") + "%";
                    break;
            }
        }
    }

}