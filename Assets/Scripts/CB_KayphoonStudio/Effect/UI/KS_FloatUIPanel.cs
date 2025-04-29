using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class KS_FloatUIPanel : MonoBehaviour
{
    public float floatDistance = 10f;  //�����ľ���
    public float floatDuration = 1f;   //һ�������ϸ�����ʱ��
    public bool floatDirection = true;
    public bool FloatingOnStart = true;
    private bool stopFloating = false;
    public Ease animationEase = Ease.InOutSine; // ������������

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
        // ����һ�����¸���������ѭ��Ч��
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
        rectTransform.DOKill();  // ֹͣ����DOTween����
    }

    private void OnDestroy()
    {
        rectTransform.DOKill();  // ֹͣ����DOTween����
    }
}
