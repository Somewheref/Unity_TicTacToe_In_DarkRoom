using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

using Sirenix.OdinInspector;
using System;

public class KS_UI_MarkableProgressBar_Controller : MonoBehaviour
{
    public Slider slider;
    public float maxValue;
    [InfoBox("Two empty game objects that mark the position of start and end of the bar")]
    public RectTransform StartPositionAnchor;
    public RectTransform EndPositionAnchor;


    [Header("SetValueAnimation")]
    public bool useAnimationOnSetValue = true;
    public float AnimDuration = 0.5f;
    public Ease AminEaseType = Ease.OutCubic;

    public Action onPreSetProgress;
    public Action onSliderFinished;

    protected class Marker
    {
        public KS_UI_MarkableProgressBar_Marker marker;
        public float value;
        public Action onPassingMarker;

        public bool passed = false;

        public void CheckPassing(float currentValue)
        {
            if (passed) return;

            if (currentValue >= value)
            {
                passed = true;
                onPassingMarker?.Invoke();
            }
        }
    }

    protected List<Marker> markers = new List<Marker>();

    /// <summary>
    /// Set the position of the child marker based on the value, should be called by KS_UI_MarkableProgressBar_Marker
    /// </summary>
    /// <param name="value"></param>
    /// <param name="self"></param>
    public void SetPosition(float value, ref KS_UI_MarkableProgressBar_Marker self, Action onPassingMarker = null)
    {
        float posRatio = Mathf.Min(value / maxValue, maxValue);
        self.transform.position = Vector3.Lerp(StartPositionAnchor.position, EndPositionAnchor.position, posRatio);

        markers.Add(new Marker()
        {
            marker = self,
            value = value,
            onPassingMarker = onPassingMarker
        });
    }


    private void Reset()
    {
        slider = GetComponent<Slider>();
    }

    [HorizontalGroup("Buttons")]
    [Button("Set Child Markers", buttonSize: ButtonSizes.Large)]
    public void Test_SetChildMarkers()
    {
        KS_UI_MarkableProgressBar_Marker[] markers = GetComponentsInChildren<KS_UI_MarkableProgressBar_Marker>();
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i].OnEnable();
        }
    }

    public void SetMaxDistance(float _maxValue)
    {
        //slider.maxValue = _maxValue;
        maxValue = _maxValue;
    }

    public void SetProgress(float value)
    {
        onPreSetProgress?.Invoke();
        
        value /= maxValue;
        if (!useAnimationOnSetValue)
        {
            slider.value = value;
            onSliderFinished?.Invoke();
            return;
        }
        
        Tween sliderTween = DOTween.To(() => slider.value, x => slider.value = x, value, AnimDuration).SetEase(AminEaseType);

        // when the tween is playing, each time the value is changed, check if any marker is passed'
        sliderTween.OnUpdate(() =>
        {
            for (int i = 0; i < markers.Count; i++)
            {
                markers[i].CheckPassing(slider.value * maxValue);
            }
        });

        sliderTween.OnComplete(() =>
        {
            onSliderFinished?.Invoke();
        });
    }

}
