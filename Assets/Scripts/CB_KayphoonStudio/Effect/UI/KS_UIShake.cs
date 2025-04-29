using UnityEngine;
using DG.Tweening;

namespace KayphoonStudio
{ 
public class KS_UIShake : MonoBehaviour
{
    public bool AutoPlayOnStart = true;
    public bool EnableScaling = true;
    public float SleepTime = 1.5f;
    public Vector3 originalScale;
    public float rotationDegree=1f;
        public float scalingDegree = 1f;
        private Sequence shakeSequence;
       

    private void Start()
    {
        originalScale = transform.localScale;
        if (AutoPlayOnStart)
        {
            Play();
        }
    }
    [ContextMenu("Play")]
    public void PlayAnim()
    {
        Play();
    }

    public void Play(int numOfLoop=-1)
    {
        if (shakeSequence != null && shakeSequence.IsPlaying())
        {
            return;
        }

        shakeSequence = DOTween.Sequence();

        // ����ҡ�κͷŴ󶯻�
        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, 15* rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree*1.1f * originalScale.x, scalingDegree * 1.1f * originalScale.y, scalingDegree * 1.1f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, -15 * rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree * 1.05f * originalScale.x, scalingDegree * 1.05f * originalScale.y, scalingDegree * 1.05f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, 10 * rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree * 1.1f * originalScale.x, scalingDegree * 1.1f * originalScale.y, scalingDegree * 1.1f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, -10 * rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree * 1.05f * originalScale.x, scalingDegree * 1.05f * originalScale.y, scalingDegree * 1.05f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, 5 * rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree * 1.1f * originalScale.x, scalingDegree * 1.1f * originalScale.y, scalingDegree * 1.1f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(new Vector3(0, 0, -5 * rotationDegree), 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(new Vector3(scalingDegree * 1.05f * originalScale.x, scalingDegree * 1.05f * originalScale.y, scalingDegree * 1.05f * originalScale.z), 0.25f).SetEase(Ease.InOutQuad));
        }

        shakeSequence.Append(transform.DORotate(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad));
        if (EnableScaling)
        {
            shakeSequence.Join(transform.DOScale(originalScale, 0.25f).SetEase(Ease.InOutQuad));
        }

        // ��Ϣ1.5��
        shakeSequence.AppendInterval(SleepTime);

        // �ظ�����
        shakeSequence.SetLoops(numOfLoop);
    }
    
    
       
    [ContextMenu("Stop")]
    public void Stop()
    {
        if (shakeSequence != null)
        {
            shakeSequence.Kill();
            shakeSequence = null;
            transform.DORotate(Vector3.zero, 0.25f).SetEase(Ease.InOutQuad);
            transform.DOScale(originalScale, 0.25f).SetEase(Ease.InOutQuad);
        }
    }
}
}