using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAudioManager : KS_Singleton<UnityAudioManager>
{
    public AudioSource BGMAudioSource;

    public void PlayBGMImmediate(AudioClip clip)
    {
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
    }

    public void FadeSwitchBGM(AudioClip clip, float duration = 1.0f)
    {
        StartCoroutine(FadeSwitchBGMCoroutine(clip, duration));
    }

    IEnumerator FadeSwitchBGMCoroutine(AudioClip clip, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            BGMAudioSource.volume = 1 - time / duration;
            time += Time.deltaTime;
            yield return null;
        }
        BGMAudioSource.volume = 0;
        BGMAudioSource.clip = clip;
        BGMAudioSource.Play();
        time = 0;
        while (time < duration)
        {
            BGMAudioSource.volume = time / duration;
            time += Time.deltaTime;
            yield return null;
        }
        BGMAudioSource.volume = 1;
    }
}
