using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace KayphoonStudio.UI
{
    /// <summary>
    /// Keeps the native aspect ratio of the image, while fitting it into the parent container
    public class RawImageContrainter : MonoBehaviour
    {
        public RawImage rawImage;
        public RectTransform rectTransform;

        [Button("Fit Image")]
        public void FitImageButton()
        {
            FitImage();
        }

        private void Reset()
        {
            rawImage = GetComponent<RawImage>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            FitImage();
        }

        private void FitImage()
        {
            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float imageWidth = rawImage.texture.width;
            float imageHeight = rawImage.texture.height;

            float ratio = imageWidth / imageHeight;

            float newWidth = parentWidth;
            float newHeight = parentWidth / ratio;

            if (newHeight > parentHeight)
            {
                newHeight = parentHeight;
                newWidth = parentHeight * ratio;
            }

            if (newWidth > parentWidth)
            {
                newWidth = parentWidth;
                newHeight = parentWidth / ratio;
            }

            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        }
    }
}