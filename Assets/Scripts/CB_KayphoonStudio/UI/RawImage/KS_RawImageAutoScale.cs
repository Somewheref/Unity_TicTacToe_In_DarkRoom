using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace KayphoonStudio.UI
{
    /// <summary>
    /// Auto scale image to fit height or width
    /// </summary>
    public class KS_RawImageAutoScale : MonoBehaviour
    {
        
        public RectTransform containerTransform;    // the transform we will try to fit the image into
        public bool scaleOnStart = true;

        public ScaleMode scaleMode = ScaleMode.FitHeight;

        private void Reset()
        {
            containerTransform = transform.parent.GetComponent<RectTransform>();
        }

        private void Start()
        {
            if(scaleOnStart)
                Scale();
        }

        [ContextMenu("Scale")]
        public void Scale()
        {
            if (containerTransform == null)
            {
                Debug.LogError("No container transform set");
                return;
            }

            var image = GetComponent<RawImage>();
            if (image == null)
            {
                Debug.LogError("No image component found");
                return;
            }

            var containerSize = new Vector2(containerTransform.rect.width, containerTransform.rect.height);
            image.SetNativeSize();
            var imageSize = new Vector2(image.texture.width, image.texture.height);

            var scale = scaleMode == ScaleMode.FitHeight
                ? containerSize.y / imageSize.y
                : containerSize.x / imageSize.x;

            Debug.Log("image size: " + imageSize + " container size: " + containerSize + " scale: " + scale);

            image.rectTransform.localScale = new Vector3(scale, scale, 1);
        }


        public enum ScaleMode
        {
            FitHeight,
            FitWidth,
        }
    }

}