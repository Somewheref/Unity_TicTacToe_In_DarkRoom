using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;

namespace DG.Tweening
{
    public static class KS_DotweenExtension
    {
        /// <summary>
        /// Moves the transform to the end position in the local space of the target transform.
        /// </summary>
        /// <param name="transform">The transform to move.</param>
        /// <param name="target">The target transform.</param>
        /// <param name="targetLocalEndPosition">The end position in the target transform's local space.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <returns>The tweener.</returns>
        /// <summary>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMoveInTargetLocalSpace(this Transform transform, Transform target, Vector3 targetLocalEndPosition, float duration)
        {
            var t = DOTween.To(
                () => transform.localPosition - target.transform.localPosition, // Value getter
                x => transform.localPosition = x + target.transform.localPosition, // Value setter
                targetLocalEndPosition + target.transform.localPosition, 
                duration);
            t.SetTarget(transform);
            return t;
        }
    }

}

public static class KS_DotweenExtension_BuiltIn
{
    public static void KS_FadeIn(this CanvasGroup canvasGroup, float duration = 1, Ease ease = Ease.Linear, bool unscaledTime = false, Action onFinish = null, bool blockRaycast = true)
    {
        if (blockRaycast)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        canvasGroup.DOFade(1, duration).SetEase(ease).OnComplete(() => {canvasGroup.KS_Show(blockRaycast: blockRaycast); onFinish?.Invoke();}).SetUpdate(unscaledTime);
    }

    public static void KS_FadeOut(this CanvasGroup canvasGroup, float duration = 1, Ease ease = Ease.Linear, bool unscaledTime = false, Action onFinish = null)
    {
        canvasGroup.DOFade(0, duration).SetEase(ease).OnComplete(() => {canvasGroup.KS_Hide(); onFinish?.Invoke();}).SetUpdate(unscaledTime);
    }
}