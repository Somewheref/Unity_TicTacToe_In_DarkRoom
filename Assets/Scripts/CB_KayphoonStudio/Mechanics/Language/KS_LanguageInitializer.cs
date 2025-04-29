using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

namespace KayphoonStudio.Language
{
    public class KS_LanguageInitializer : MonoBehaviour
    {
        [Header("自身的Text组件（适用于自改变类）")]
        public TMP_Text text;
        [Header("隐藏式组件展示语言设置")]
        public string DisplayLang = "en";
        [Header("组件内自翻译设置")]
        public bool UseSelfLangInfo = false;
        public string enContent="";
        public string chContent = "";
        [Header("其他设置")]
        public bool DisableLangFunc = false;
        // added by Stanly
        public bool UpdateFontOnly = false;
        public bool UpdateContentOnly = false;

        public UnityEvent OnLanguageSelected;

        private List<GameObject> childUIText;
        /// <summary>
        /// UI自启动翻译器
        /// </summary>
        void Start()
        {
            if (DisableLangFunc)
            {
                return;
            }

            if (UpdateContentOnly)
            {
                UpdateText();
                return;
            }

            KS_NotificationCenter.AddEventListener("ChangeLang", OnLangChanged);
            if (!text) { 
            text= GetComponent<TMP_Text>();
            }
            if (gameObject.CompareTag("UILangBlock")|| gameObject.CompareTag("UITexts"))
            {
                childUIText = gameObject.GetChildCollection("UISubText");
            }
            UpdateText();
        }

        public void OnLangChanged(Notification notific)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            if (gameObject.CompareTag("UIText"))
            {
                if (UseSelfLangInfo)
                {
                    switch(KS_Language.Instance.CurrentLang){
                        case "en":
                            text.text = enContent;
                            break;
                        case "ch":
                            text.text = chContent;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (UpdateContentOnly)
                    {
                        text.text = KS_Language.Instance.GetText(gameObject.name);
                    }
                    else if (UpdateFontOnly)
                    {
                        text.font = KS_Language.Instance.GetCurrentData().defaultFont;
                    }
                    else
                    {
                        text.text = KS_Language.Instance.GetText(gameObject.name);
                        text.font = KS_Language.Instance.GetCurrentData().defaultFont;
                    }
                }
            }
            else if (gameObject.CompareTag("UILangBlock"))
            {
                UpdateLangBlock();
            }
            else if (gameObject.CompareTag("UISubText"))
            {
                UpdateSubText();
            }
            else if (gameObject.CompareTag("UITexts"))
            {
                UpdateTexts();
            }

            if (KS_Language.Instance.CurrentLang == DisplayLang)
            {
                OnLanguageSelected?.Invoke();
            }
        }

        public void UpdateTexts()
        {
            foreach (GameObject childObj in childUIText)
            {
                var text = childObj.GetComponent<TMP_Text>();
                if (text)
                {
                    if (!UpdateFontOnly)
                        text.text = KS_Language.Instance.GetText(childObj.name);
                    text.font = KS_Language.Instance.GetCurrentData().defaultFont;
                }
            }
        }

        public void UpdateLangBlock()
        {
            bool state=DisplayLang == KS_Language.Instance.CurrentLang;
            foreach(GameObject childObj in childUIText)
            {
                childObj.SetActive(state);
            }
        }

        public void UpdateSubText()
        {
            bool state = DisplayLang == KS_Language.Instance.CurrentLang;
            //gameObject.SetActive(state);
            text.enabled = state;
        }

        [ContextMenu("GetSelfText")]
        void GetSelfText()
        {
            text = gameObject.GetComponent<TMP_Text>();
        }
    }
}