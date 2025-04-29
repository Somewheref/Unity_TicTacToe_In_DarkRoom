using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace KayphoonStudio.UI
{  
    /// <summary>
    /// Tap anywhere to continue UI.
    /// </summary>
    public class KS_TapAnywhereUI : MonoBehaviour
    {
        public UnityEvent OnTapAnywhere;
        
        public bool disableOnTap = true;
        public bool PConly = true;
        public bool tempDisable = false;
        private bool _isTapped = false;

        public void TempDisable()
        {
            tempDisable = true;
        }

        public void TempUndisable()
        {
            tempDisable = false;
        }

        private void Update() 
        {
            if (_isTapped) return;

            if (Input.anyKey && Input.touchCount == 0)
            {
                // check if the touch is on UI
                if (EventSystem.current.IsPointerOverGameObject()) return;

                _isTapped = true;
                if(!tempDisable)
                    OnTapAnywhere.Invoke();
                if (disableOnTap) gameObject.SetActive(false);
            }

            if (PConly) return;

            if (Input.touchCount > 0)
            {
                // check if the touch is on UI
                foreach (Touch touch in Input.touches)
                {
                    int id = touch.fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(id))
                    {
                        // ui touched
                        return;
                    }
                }

                if(EventSystem.current.IsPointerOverGameObject(0)) return;

                // also raycast to check if the touch is on UI
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
                    {
                        return;
                    }
                }

                _isTapped = true;
                if (!tempDisable)
                    OnTapAnywhere.Invoke();
                if (disableOnTap) gameObject.SetActive(false);
            }
        }
    }

}

