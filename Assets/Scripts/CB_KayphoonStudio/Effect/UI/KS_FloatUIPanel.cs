using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class KS_FloatUIPanel : MonoBehaviour
{
    public float floatDistance = 10f;  //浮动的距离
    public float floatDuration = 1f;   //一个方向上浮动的时间
    public bool floatDirection = true;
    public bool FloatingOnStart = true;
    private bool stopFloating = false;
    public Ease animationEase = Ease.InOutSine; // 动画曲线类型

    public RectTransform rectTransform;
    private int directionInt =1;

    private void Start()
    {
        if(rectTransform==null)
            rectTransform = GetComponent<RectTransform>();
        if (FloatingOnStart)
        {
            StartFloating();
        }
        if (!floatDirection)
            directionInt = -1;
    }

    [ContextMenu("StartFloating")]
    public void StartFloating()
    {
        // 创建一个上下浮动的无限循环效果
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + directionInt * floatDistance, floatDuration).SetEase(animationEase).OnComplete(() =>
        {
            if (stopFloating)
            {
                rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - directionInt * floatDistance, floatDuration).SetEase(animationEase);
            }
            else
            {
                rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - directionInt * floatDistance, floatDuration).SetEase(animationEase).OnComplete(StartFloating);
            }
        });
    }

    [ContextMenu("StopFloating")]
    public void StopFloating()
    {
        stopFloating = true;
    }

    [ContextMenu("ForceStopFloating")]
    public void ForceStopFloating()
    {
        rectTransform.DOKill();  // 停止所有DOTween动画
    }

    private void OnDestroy()
    {
        rectTransform.DOKill();  // 停止所有DOTween动画
    }
}
