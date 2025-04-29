using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

namespace KayphoonStudio
{

    public class KS_ImageFillTween : MonoBehaviour
    {
        public Image img;
        public float initFill = 0;
        public float targetFill = 1;
        public float duration = 5;
        public Ease ease = Ease.Linear;

        public bool startOnEnable = false;


        private void Reset()
        {
            img = GetComponent<Image>();
        }

        private void OnEnable()
        {
            img.fillAmount = initFill;
            if (startOnEnable)
            {
                StartTween();
            }
        }

        public void FadeOut()
        {
            if (!TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.KS_FadeOut();
        }

        public void StartTween()
        {
            img.DOFillAmount(targetFill, duration).SetEase(ease);
        }
    }
}
