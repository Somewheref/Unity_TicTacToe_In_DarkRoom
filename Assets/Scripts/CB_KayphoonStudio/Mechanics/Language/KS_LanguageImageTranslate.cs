using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KayphoonStudio.Language
{
    public class KS_LanguageImageTranslate : MonoBehaviour
    {
        public Image image;

        [System.Serializable]
        public class LanguageImage
        {
            public string language;
            public Sprite image;
        }
        
        public List<LanguageImage> languageImages;

        private void Reset() {
            image = GetComponent<Image>();
        }
        
        public void TranslateImage()
        {
            foreach (LanguageImage languageImage in languageImages)
            {
                if (languageImage.language == KS_Language.Instance.CurrentLang)
                {
                    image.sprite = languageImage.image;
                    return;
                }
            }
        }
    }
}
