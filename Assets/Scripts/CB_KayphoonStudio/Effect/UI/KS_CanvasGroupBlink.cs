using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_CanvasGroupBlink : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float blinkInterval = 0.5f; 
    public bool blinkOnStart = true;
    public float maxAlpha = 1f;
    public float minAlpha = 0f;
    public bool IgnoreTimeScale = false;

    private void Reset() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        maxAlpha = 1f;
        minAlpha = 0f;
    }

    private void OnEnable() {
        if (blinkOnStart)
        {
            StartBlinking();
        }
    }

    public void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        while (true)
        {
            canvasGroup.alpha = maxAlpha;
            if (IgnoreTimeScale)
                yield return new WaitForSecondsRealtime(blinkInterval);
            else
                yield return new WaitForSeconds(blinkInterval);
            canvasGroup.alpha = minAlpha;
            if (IgnoreTimeScale)
                yield return new WaitForSecondsRealtime(blinkInterval);
            else
                yield return new WaitForSeconds(blinkInterval);
        }
    }
}
