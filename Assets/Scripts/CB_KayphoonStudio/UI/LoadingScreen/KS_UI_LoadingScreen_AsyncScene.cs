using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using System;

namespace KayphoonStudio.UI
{
    public class KS_UI_LoadingScreen_AsyncScene : KS_Singleton<KS_UI_LoadingScreen_AsyncScene>
    {
        public CanvasGroup CanvasGroup;

        public float FadeInTime = 0.5f;
        public float FadeOutTime = 0.5f;

        private void Reset() 
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            CanvasGroup.KS_Hide();
        }

        public void FadeIn(Action onComplete = null)
        {
            CanvasGroup.DOFade(1, FadeInTime).OnComplete(() => {
                onComplete?.Invoke();
            });
        }

        public void FadeOut(Action onComplete = null)
        {
            CanvasGroup.DOFade(0, FadeOutTime).OnComplete(() => {
                onComplete?.Invoke();
            });
        }
    }

}