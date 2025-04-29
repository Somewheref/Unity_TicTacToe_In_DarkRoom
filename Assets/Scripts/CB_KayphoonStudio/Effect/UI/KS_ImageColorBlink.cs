using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KayphoonStudio.UI
{
    public class KS_ImageColorBlink : MonoBehaviour
    {
        public Image image;
        public Color normalColor;
        public Color blinkColor;
        public float blinkInterval = 0.5f;
        public bool blinkOnStart = false;

        private void Reset() {
            image = GetComponent<Image>();
            normalColor = image.color;
            blinkColor = Color.white;
        }

        private void OnEnable() {
            if (blinkOnStart)
            {
                StartBlinking();
            }
        }
        
        public void StartBlinking()
        {
            StopAllCoroutines();
            StartCoroutine(Blinking());
        }

        IEnumerator Blinking()
        {
            while (true)
            {
                image.color = blinkColor;
                yield return new WaitForSeconds(blinkInterval);
                image.color = normalColor;
                yield return new WaitForSeconds(blinkInterval);
            }
        }

    }
}