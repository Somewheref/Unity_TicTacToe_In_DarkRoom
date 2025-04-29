using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_TextBreathingEffect : MonoBehaviour
    {
        public float maxScale = 1.2f;  // 最大放大倍数
        public float speed = 2f;       // 呼吸速度
        public bool AutoPlayOnStart = true;
        public Vector3 originalScale;
        public bool IsBreathing { get; private set; }
        public bool ForceUseUnityScale = false;
        private void Start()
        {
            originalScale = transform.localScale;
            if(AutoPlayOnStart)
                StartBreathing();
            if (!AutoPlayOnStart||ForceUseUnityScale)
            {
                originalScale = Vector3.one;
            }
        }
        [ContextMenu("Start")]
        public void StartBreathing()
        {
           transform.DOScale(originalScale * maxScale, speed)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
        [ContextMenu("Stop")]
        public void StopBreathing()
        {
            transform.DOKill();
            transform.localScale = originalScale;
        }
    }
}
