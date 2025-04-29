using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Sirenix.OdinInspector;

namespace KayphoonStudio.Language
{
    public class TMP_LanguageReplacer : MonoBehaviour
    {
        public TMP_Text text;
        //public LanguageManager.Languages defaultLanguage = LanguageManager.Languages.English;

        [Header("English")]
        public TMP_FontAsset englishFont;
        [TextArea]
        public string englishText;
        [Button("Preview English")]
        public void PreviewEnglish()
        {
            text.text = englishText;
            text.font = englishFont;
        }

        [Header("Chinese")]
        public TMP_FontAsset chineseFont;
        [TextArea]
        public string chineseText;
        [Button("Preview Chinese")]
        public void PreviewChinese()
        {
            text.text = chineseText;
            text.font = chineseFont;
        }


        private void Reset()
        {
            text = GetComponent<TMP_Text>();
            englishText = text.text;

            // if (defaultLanguage == LanguageManager.Languages.English)
            // {
                englishFont = text.font;
                // chineseFont = LanguageManager.DefaultChineseFont;
            // }
            // else
            // {
            //     englishFont = LanguageManager.DefaultEnglishFont;
            //     chineseFont = text.font;
            // }
        }

        private void Awake()
        {
            UpdateLanguage();
        }

        public void UpdateLanguage()
        {
            // switch (LanguageManager.currentLanguage)
            // {
            //     case LanguageManager.Languages.English:
            //         text.text = englishText;
            //         text.font = englishFont;
            //         break;

            //     case LanguageManager.Languages.Chinese:
            //         text.text = chineseText;
            //         text.font = chineseFont;
            //         break;
            // }
        }
    }
}