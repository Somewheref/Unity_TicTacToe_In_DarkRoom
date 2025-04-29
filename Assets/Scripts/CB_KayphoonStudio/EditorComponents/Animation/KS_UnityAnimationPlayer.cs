using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace KayphoonStudio.EditorUtilities
{
    public class KS_UnityAnimationPlayer : MonoBehaviour
    {
        public UnityEngine.Animation anim;

        public UnityEvent OnAnimationStarted;

        private void Reset() {
            anim = GetComponent<UnityEngine.Animation>();
        }

        public void PlayAnimation(AnimationClip clip)
        {
            anim.Play(clip.name);

            OnAnimationStarted?.Invoke();
        }
    }
}
