using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace KayphoonStudio
{
    public class KS_TextCountdown : MonoBehaviour
    {
        public TMP_Text numberText;
        public string endingText = "0";
        public int countdownTime = 5;
        public bool countOnEnable = false;
        [Tooltip("If true, the text field will be set to the countdown time on enable.")]
        public bool initNumberOnEnable = true;

        public UnityEvent onCountdownEnd;

        private bool isCounting = false;


        private void Reset() 
        {
            numberText = GetComponent<TMP_Text>();
        }

        private void OnEnable() 
        {
            if (countOnEnable)
            {
                StartCountdown();
            }

            if (initNumberOnEnable)
            {
                numberText.text = countdownTime.ToString();
            }
        }

        public void StartCountdown()
        {
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            if (isCounting)
            {
                KS_Logger.LogWarning("Countdown is already running, neglecting new request to start countdown.", this);
                yield break;
            }
            isCounting = true;
            int currentTime = countdownTime;
            while (currentTime > 0)
            {
                numberText.text = currentTime.ToString();
                yield return new WaitForSeconds(1);
                currentTime--;
            }
            numberText.text = "0";
            isCounting = false;

            onCountdownEnd?.Invoke();
        }
    }
}
