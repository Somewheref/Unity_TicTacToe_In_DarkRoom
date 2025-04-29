using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayphoonStudio;

namespace DG.Tweening
{
    public class KS_DotweenConfigurer
    {
        static readonly bool showLog = false;
        const int MaxTweenCapacity = 300;
        const int MaxSequenceCapacity = 50;

        [RuntimeInitializeOnLoadMethod]
        static void OnRuntimeInitialized()
        {
            DOTween.SetTweensCapacity(tweenersCapacity: MaxTweenCapacity, sequencesCapacity: MaxSequenceCapacity);

            if (showLog)
            {
                KS_Logger.Log("DOTweenConfigurer: TweensCapacity: " + MaxTweenCapacity + ", SequencesCapacity: " + MaxSequenceCapacity, sender: "KS_DotweenConfigurer");
            }
        }
    }
}
