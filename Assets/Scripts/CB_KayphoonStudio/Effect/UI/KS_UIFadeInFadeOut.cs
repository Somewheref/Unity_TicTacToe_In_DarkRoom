using UnityEngine;
using DG.Tweening; // ����DoTween�����ռ�

namespace KayphoonStudio
{
    public class KS_UIFadeInFadeOut : MonoBehaviour
    {
        public CanvasGroup canvasGroup; // ��Ҫ���뵭����CanvasGroup
        public float fadeInDuration = 1.0f; // ���뵭���ĳ���ʱ��
        public float fadeOutDuration = 1.0f; // ���뵭���ĳ���ʱ��
        // Start is called before the first frame update
        void Start()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
            if (canvasGroup == null)
            {
                // add a CanvasGroup component if it doesn't exist
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        [ContextMenu("FadeIn")]
        public void FadeIn()
        {
            canvasGroup.KS_FadeIn(fadeInDuration);
        }

        [ContextMenu("FadeOut")]
        public void FadeOut()
        {
            canvasGroup.KS_FadeOut(fadeOutDuration);
        }
    }
}
