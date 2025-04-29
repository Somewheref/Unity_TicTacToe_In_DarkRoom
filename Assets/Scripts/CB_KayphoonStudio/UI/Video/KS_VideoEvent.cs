using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.Video;

namespace KayphoonStudio.Video
{
    public class KS_VideoEvent : MonoBehaviour
    {
        public VideoPlayer videoPlayer;

        public List<VideoEvent> videoEvents = new();

        public UnityEvent OnVideoEnd;

        [System.Serializable]
        public class VideoEvent
        {
            [Range(0, 1)]
            public float time;
            public UnityEvent onEvent;

            protected bool triggered = false;

            public void Trigger()
            {
                if (triggered) return;

                triggered = true;
                onEvent?.Invoke();
            }
        }

        private void Reset() {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start() {
            videoPlayer.started += (VideoPlayer vp) => OnVideoStart();
            videoPlayer.loopPointReached += (VideoPlayer vp) => OnVideoEnd?.Invoke();
        }

        private void OnVideoStart() 
        {
            StartCoroutine(VideoEventCoroutine());
        }

        private IEnumerator VideoEventCoroutine()
        {
            while (videoPlayer.isPlaying)
            {
                foreach (var item in videoEvents)
                {
                    if (videoPlayer.time / videoPlayer.length >= item.time)
                    {
                        item.Trigger();
                    }
                }
                yield return null;
            }
        }
    }
}


