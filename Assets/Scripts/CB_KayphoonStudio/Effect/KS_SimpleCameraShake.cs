using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_SimpleCameraShake : MonoBehaviour
    {
        // �����ĳ���ʱ��
        public float shakeDuration = 0.5f;
        // ������ǿ��
        public float shakeAmount = 0.7f;
        // �������ӵ��ٶ�
        public float decreaseFactor = 1.0f;

        private Vector3 originalPos;
        private float currentShakeDuration = 0f;

        void OnEnable()
        {
            originalPos = transform.localPosition;
        }

        void Update()
        {
            if (currentShakeDuration > 0)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                currentShakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                currentShakeDuration = 0f;
                transform.localPosition = originalPos;
            }
        }
        [ContextMenu("TriggerShake")]
        public void TriggerShake()
        {
            currentShakeDuration = shakeDuration;
        }
    }
}
