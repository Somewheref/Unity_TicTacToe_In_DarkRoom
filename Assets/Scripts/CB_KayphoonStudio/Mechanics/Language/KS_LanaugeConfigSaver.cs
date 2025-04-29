using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KayphoonStudio.Language
{
    public class KS_LanaugeConfigSaver : MonoBehaviour
    {
        [Header("Debug")]
        public string debug_language = "en";
        public List<TMP_TextSettings> textSettings = new();

        [System.Serializable]
        public class TMP_TextSettings
        {
            [Header("Language")]
            public string language;

            [Header("TMP Text Settings")]
            public TMP_FontAsset fontAsset;
            public Material fontMaterial;
            public FontStyles fontStyle;
            public float fontSize;
            public bool autoSize;
            public float fontSizeMin;
            public float fontSizeMax;
            public float characterSpacing;
            public float wordSpacing;
            public float lineSpacing;
            public float paragraphSpacing;
            public bool enableWordWrapping;
            public TextAlignmentOptions alignment;
            // public string text;

            public TMP_TextSettings(TMP_Text tmp)
            {
                fontAsset = tmp.font;
                fontMaterial = tmp.fontMaterial;
                fontStyle = tmp.fontStyle;
                fontSize = tmp.fontSize;
                autoSize = tmp.enableAutoSizing;
                fontSizeMin = tmp.fontSizeMin;
                fontSizeMax = tmp.fontSizeMax;
                characterSpacing = tmp.characterSpacing;
                wordSpacing = tmp.wordSpacing;
                lineSpacing = tmp.lineSpacing;
                paragraphSpacing = tmp.paragraphSpacing;
                enableWordWrapping = tmp.enableWordWrapping;
                alignment = tmp.alignment;
                // text = tmp.text;
            }

            public void ApplyToTMP_Text(TMP_Text tmp)
            {
                tmp.font = fontAsset;
                tmp.fontMaterial = fontMaterial;
                tmp.fontStyle = fontStyle;
                tmp.fontSize = fontSize;
                tmp.enableAutoSizing = autoSize;
                tmp.fontSizeMin = fontSizeMin;
                tmp.fontSizeMax = fontSizeMax;
                tmp.characterSpacing = characterSpacing;
                tmp.wordSpacing = wordSpacing;
                tmp.lineSpacing = lineSpacing;
                tmp.paragraphSpacing = paragraphSpacing;
                tmp.enableWordWrapping = enableWordWrapping;
                tmp.alignment = alignment;
                // tmp.text = text;
            }
        }

        private void Start() 
        {
            KS_NotificationCenter.AddEventListener(KS_InternalNotificationKeys.OnLanguageChanged, OnLanguageChanged);
        }

        void OnLanguageChanged(Notification notification)
        {
            string language = notification.param as string;

            ApplyLanguageSettings(language);
        }

        public void ApplyLanguageSettings(string language)
        {
            bool applied = false;

            foreach (var textSettings in textSettings)
            {
                if (textSettings.language == language)
                {
                    textSettings.ApplyToTMP_Text(GetComponent<TMP_Text>());
                    applied = true;
                }
            }

            if (!applied)
            {
                KS_Logger.LogWarning("Language not found: " + language, this);
            }
        }


        [ContextMenu("Debug Apply Language Settings")]
        public void Debug_ApplyLanguageSettings()
        {
            ApplyLanguageSettings(debug_language);
        }


        [ContextMenu("Save TMP Text Settings")]
        public void SaveTMPTextSettings()
        {
            textSettings.Add(new TMP_TextSettings(GetComponent<TMP_Text>()));
        }

    }
}
