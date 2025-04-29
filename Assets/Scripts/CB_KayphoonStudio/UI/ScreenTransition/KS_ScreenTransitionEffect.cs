using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KayphoonStudio.UI
{
    /// <summary>
    /// Transition effects such as fade in/out, slide in/out, etc.
    /// </summary>
    public class KS_ScreenTransitionEffect : KS_Singleton<KS_ScreenTransitionEffect>
    {
        public CanvasGroup EffectImage;
        public GameObject WhiteImage;
        public GameObject BlackImage;
        public bool DonotDestroyOnLoad = true;

        protected Canvas canvas;
        protected int sortingOrder = 1000;

        private void Awake()
        {
            // Make sure there is only one instance of this object
            if (FindObjectsOfType<KS_ScreenTransitionEffect>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            if (DonotDestroyOnLoad && transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }

            canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                KS_Logger.LogError("Canvas not found", this);
            }
            else
            {
                sortingOrder = canvas.sortingOrder;
            }
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public void FadeIn(float duration, float delay = 0f, System.Action callback = null, EffectPreset effectPreset = EffectPreset.WhiteImage)
        {
            Show();
            SelectEffect(effectPreset);
            // make sure canvas group do not block raycast after fade in
            callback += () => EffectImage.blocksRaycasts = false;
            EffectImage.DOFade(0f, duration).SetDelay(delay).OnComplete(() => {Hide(); callback?.Invoke(); });
        }

        public void FadeOut(float duration, float delay = 0f, System.Action callback = null, EffectPreset effectPreset = EffectPreset.WhiteImage)
        {
            Show();
            SelectEffect(effectPreset);
            // make sure canvas group block raycast after fade out
            EffectImage.blocksRaycasts = true;
            
            TemporarilyOnTop(duration + delay);

            EffectImage.DOFade(1f, duration).SetDelay(delay).OnComplete(() => {Show(); callback?.Invoke(); });
        }

        public void FadeOutThenIn(float duration, float delay = 0f, System.Action middlecallback = null, System.Action completedCallback = null, EffectPreset effectPreset = EffectPreset.WhiteImage)
        {
            Show();
            SelectEffect(effectPreset);

            TemporarilyOnTop(duration + delay);

            EffectImage.DOFade(1f, duration).SetDelay(delay).OnComplete(() => {
                middlecallback?.Invoke();
                EffectImage.DOFade(0f, duration).OnComplete(() => {Hide(); completedCallback?.Invoke(); });
            });
        }

        public enum EffectPreset
        {
            WhiteImage,
            BlackImage
        }

        public void SelectEffect(EffectPreset effectPreset)
        {
            switch (effectPreset)
            {
                case EffectPreset.WhiteImage:
                    WhiteImage.SetActive(true);
                    BlackImage.SetActive(false);
                    break;
                case EffectPreset.BlackImage:
                    WhiteImage.SetActive(false);
                    BlackImage.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        
        protected void TemporarilyOnTop(float duration)
        {
            // make the canvas group on top of everything
            SetSortingOrder(1000);
            Invoke(nameof(ResetSortingOrder), duration);
        }

        protected void ResetSortingOrder()
        {
            CancelInvoke(nameof(SetSortingOrder));
            canvas.sortingOrder = sortingOrder;
        }

        protected void SetSortingOrder(int sortingOrder)
        {
            CancelInvoke(nameof(ResetSortingOrder));
            canvas.sortingOrder = sortingOrder;
        }
    }

}