using System.Collections;
using UnityEngine;
using DG.Tweening;

//[RequireComponent(typeof(RectTransform))]
//public class KS_FloatUIPanel : MonoBehaviour
//{
//    public float floatDistance = 10f;  //�����ľ���
//    public float floatDuration = 1f;   //һ�������ϸ�����ʱ��

//    public RectTransform rectTransform;

//    private void Start()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        StartFloating();
//    }

//    private void StartFloating()
//    {
//        // ����һ�����¸���������ѭ��Ч��
//        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + floatDistance, floatDuration).SetEase(Ease.InOutSine).OnComplete(() =>
//        {
//            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - floatDistance, floatDuration).SetEase(Ease.InOutSine).OnComplete(StartFloating);
//        });
//    }

//    private void OnDestroy()
//    {
//        rectTransform.DOKill();  // ֹͣ����DOTween����
//    }
//}
