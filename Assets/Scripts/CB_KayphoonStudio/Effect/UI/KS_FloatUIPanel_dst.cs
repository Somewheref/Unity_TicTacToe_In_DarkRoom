using System.Collections;
using UnityEngine;
using DG.Tweening;

//[RequireComponent(typeof(RectTransform))]
//public class KS_FloatUIPanel : MonoBehaviour
//{
//    public float floatDistance = 10f;  //浮动的距离
//    public float floatDuration = 1f;   //一个方向上浮动的时间

//    public RectTransform rectTransform;

//    private void Start()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        StartFloating();
//    }

//    private void StartFloating()
//    {
//        // 创建一个上下浮动的无限循环效果
//        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + floatDistance, floatDuration).SetEase(Ease.InOutSine).OnComplete(() =>
//        {
//            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - floatDistance, floatDuration).SetEase(Ease.InOutSine).OnComplete(StartFloating);
//        });
//    }

//    private void OnDestroy()
//    {
//        rectTransform.DOKill();  // 停止所有DOTween动画
//    }
//}
