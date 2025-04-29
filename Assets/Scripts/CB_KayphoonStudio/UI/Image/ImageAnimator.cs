using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class ImageAnimator : MonoBehaviour
{
    [Title("Image Sequence Settings")]
    [ListDrawerSettings(ShowIndexLabels = true, OnBeginListElementGUI = "DrawPreviewLabel")]
    public List<Sprite> sequenceFrames = new List<Sprite>();

    [Title("Playback Settings")]
    [MinValue(0)] public float framesPerSecond = 12f;
    public bool playOnStart = true;
    public bool loop = true;
    [ShowIf("loop")] public bool pingPong = false;
    
    [Title("Runtime Controls")]
    [SerializeField, ReadOnly] private int currentFrame = 0;
    [SerializeField, ReadOnly] private bool isPlaying = false;
    [SerializeField, ReadOnly] private bool isReversed = false;

    private Image targetImage;
    private float frameTimer = 0f;
    private float frameDuration;
    private Coroutine playbackCoroutine;

    #if UNITY_EDITOR
    [Title("Preview"), PropertyOrder(100)]
    [SerializeField, PreviewField(Height = 100)]
    private Sprite previewSprite;
    
    [Button("Previous Frame"), PropertyOrder(101)]
    private void PreviewPreviousFrame()
    {
        if (sequenceFrames == null || sequenceFrames.Count == 0) return;
        currentFrame = (currentFrame - 1 + sequenceFrames.Count) % sequenceFrames.Count;
        UpdatePreview();
    }

    [Button("Next Frame"), PropertyOrder(101)]
    private void PreviewNextFrame()
    {
        if (sequenceFrames == null || sequenceFrames.Count == 0) return;
        currentFrame = (currentFrame + 1) % sequenceFrames.Count;
        UpdatePreview();
    }

    [Button("Play Preview"), PropertyOrder(102)]
    private void PlayPreview()
    {
        if (Application.isPlaying) return;
        StopPreview();
        UnityEditor.EditorApplication.update += EditorUpdate;
    }

    [Button("Stop Preview"), PropertyOrder(102)]
    private void StopPreview()
    {
        if (Application.isPlaying) return;
        UnityEditor.EditorApplication.update -= EditorUpdate;
    }

    private void EditorUpdate()
    {
        frameTimer += Time.realtimeSinceStartup - lastEditorTime;
        lastEditorTime = Time.realtimeSinceStartup;

        if (frameTimer >= frameDuration)
        {
            frameTimer = 0f;
            PreviewNextFrame();
            UnityEditor.SceneView.RepaintAll();
        }
    }

    private float lastEditorTime;

    private void UpdatePreview()
    {
        if (sequenceFrames == null || sequenceFrames.Count == 0) return;
        previewSprite = sequenceFrames[currentFrame];
        if (!Application.isPlaying && targetImage != null)
        {
            targetImage.sprite = previewSprite;
        }
    }

    private void DrawPreviewLabel(int index)
    {
        if (sequenceFrames == null || index >= sequenceFrames.Count) return;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.gray;
        UnityEditor.EditorGUILayout.LabelField($"Frame {index}", style);
    }

    private void OnValidate()
    {
        if (targetImage == null) targetImage = GetComponent<Image>();
        frameDuration = 1f / Mathf.Max(framesPerSecond, 0.001f);
    }
    #endif

    private void Awake()
    {
        targetImage = GetComponent<Image>();
        frameDuration = 1f / Mathf.Max(framesPerSecond, 0.001f);
    }

    private void Start()
    {
        if (playOnStart) Play();
    }

    private void OnEnable()
    {
        if (playbackCoroutine != null && isPlaying)
            Play();
    }

    private void OnDisable()
    {
        Stop();
    }

    [Button("Play")]
    public void Play()
    {
        if (sequenceFrames == null || sequenceFrames.Count == 0) return;
        
        Stop();
        isPlaying = true;
        playbackCoroutine = StartCoroutine(PlaySequence());
    }

    [Button("Stop")]
    public void Stop()
    {
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);
            playbackCoroutine = null;
        }
        isPlaying = false;
    }

    [Button("Reset")]
    public void Reset()
    {
        currentFrame = 0;
        isReversed = false;
        if (targetImage != null && sequenceFrames.Count > 0)
            targetImage.sprite = sequenceFrames[0];
    }

    private IEnumerator PlaySequence()
    {
        while (isPlaying)
        {
            if (targetImage != null && sequenceFrames.Count > 0)
                targetImage.sprite = sequenceFrames[currentFrame];

            yield return new WaitForSeconds(frameDuration);

            if (pingPong)
            {
                if (!isReversed)
                {
                    currentFrame++;
                    if (currentFrame >= sequenceFrames.Count - 1)
                    {
                        isReversed = true;
                        currentFrame = sequenceFrames.Count - 1;
                    }
                }
                else
                {
                    currentFrame--;
                    if (currentFrame <= 0)
                    {
                        isReversed = false;
                        currentFrame = 0;
                        if (!loop)
                        {
                            Stop();
                            yield break;
                        }
                    }
                }
            }
            else
            {
                currentFrame = (currentFrame + 1) % sequenceFrames.Count;
                if (currentFrame == 0 && !loop)
                {
                    Stop();
                    yield break;
                }
            }
        }
    }

    // Public control methods
    public void SetFPS(float fps) => framesPerSecond = fps;
    public void SetLoop(bool shouldLoop) => loop = shouldLoop;
    public void SetPingPong(bool shouldPingPong) => pingPong = shouldPingPong;
    
    [Button("Jump To Frame")]
    public void JumpToFrame(int frame)
    {
        if (sequenceFrames == null || sequenceFrames.Count == 0) return;
        currentFrame = Mathf.Clamp(frame, 0, sequenceFrames.Count - 1);
        if (targetImage != null)
            targetImage.sprite = sequenceFrames[currentFrame];
    }
}
