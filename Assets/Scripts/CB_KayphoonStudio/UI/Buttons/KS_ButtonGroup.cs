using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KayphoonStudio
{
    public class KS_ButtonGroup : MonoBehaviour
    {
        public Button[] buttons;
        public Color selectedColor = Color.white;
        public Color unselectedColor = Color.gray;
        public int selectedIndex = 0;
        public bool allowReselect = false;
        public KS_CustomUnityEvents.KS_UnityEventInt onButtonSelected;
        public bool callEventOnStart = false;

        private void Reset() 
        {
            buttons = GetComponentsInChildren<Button>();
        }

        private void Start()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    if (selectedIndex == index && !allowReselect)
                    {
                        return;
                    }
                    selectedIndex = index;
                    UpdateButtonColor();
                    onButtonSelected?.Invoke(selectedIndex);
                });
            }
            UpdateButtonColor();

            if (callEventOnStart)
            {
                onButtonSelected?.Invoke(selectedIndex);
            }
        }

        private void UpdateButtonColor()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedIndex)
                {
                    buttons[i].KS_SetNormalColor(selectedColor);
                }
                else
                {
                    buttons[i].KS_SetNormalColor(unselectedColor);
                }
            }
        }
    }
}
