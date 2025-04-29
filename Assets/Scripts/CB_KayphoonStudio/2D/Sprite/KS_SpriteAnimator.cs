using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class KS_SpriteAnimator : MonoBehaviour
{
    [System.Serializable]
    public class SpriteAnimation
    {
        public string name;
        [ListDrawerSettings(ShowIndexLabels = true)]
        public List<Sprite> frames = new List<Sprite>();
        [MinValue(0)] public float fps = 12f;
        public bool loop = true;
        public bool pingPong = false;
    }

    [Title("Animation Setup")]
    [TableList(ShowIndexLabels = true)]
    public List<SpriteAnimation> animations = new List<SpriteAnimation>();

    [Title("Runtime Settings")]
    [SerializeField] private string defaultAnimation = "";
    [SerializeField] private bool playOnStart = true;
    
    [Title("Debug Info")]
    [ReadOnly] public string currentAnimation = "";
    [ReadOnly] public int currentFrame = 0;
    [ReadOnly] public bool isPlaying = false;
    [ReadOnly] public bool isPaused = false;
    [ReadOnly] public bool isReversed = false;

    private SpriteRenderer spriteRenderer;
    private Coroutine animationCoroutine;
    private SpriteAnimation activeAnimation;
    private float frameTimer = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (playOnStart && !string.IsNullOrEmpty(defaultAnimation))
        {
            Play(defaultAnimation);
        }
    }

    private void OnDisable()
    {
        Stop();
    }

    [Button("Play")]
    public void Play(string animationName)
    {
        var animation = animations.Find(a => a.name == animationName);
        if (animation == null || animation.frames.Count == 0)
        {
            Debug.LogWarning($"Animation '{animationName}' not found or has no frames!");
            return;
        }

        Stop();
        activeAnimation = animation;
        currentAnimation = animationName;
        isPlaying = true;
        isPaused = false;
        currentFrame = 0;
        isReversed = false;
        
        animationCoroutine = StartCoroutine(AnimationRoutine());
    }

    [Button("Stop")]
    public void Stop()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
        isPlaying = false;
        isPaused = false;
    }

    [Button("Pause")]
    public void Pause()
    {
        isPaused = true;
    }

    [Button("Resume")]
    public void Resume()
    {
        isPaused = false;
    }

    private IEnumerator AnimationRoutine()
    {
        while (isPlaying)
        {
            if (!isPaused && activeAnimation != null)
            {
                // Update sprite
                if (currentFrame < activeAnimation.frames.Count)
                {
                    spriteRenderer.sprite = activeAnimation.frames[currentFrame];
                }

                // Wait for next frame
                yield return new WaitForSeconds(1f / activeAnimation.fps);

                // Update frame counter
                if (activeAnimation.pingPong)
                {
                    if (!isReversed)
                    {
                        currentFrame++;
                        if (currentFrame >= activeAnimation.frames.Count - 1)
                        {
                            isReversed = true;
                            currentFrame = activeAnimation.frames.Count - 1;
                        }
                    }
                    else
                    {
                        currentFrame--;
                        if (currentFrame <= 0)
                        {
                            isReversed = false;
                            currentFrame = 0;
                            if (!activeAnimation.loop)
                            {
                                Stop();
                            }
                        }
                    }
                }
                else
                {
                    currentFrame = (currentFrame + 1) % activeAnimation.frames.Count;
                    if (currentFrame == 0 && !activeAnimation.loop)
                    {
                        Stop();
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    #if UNITY_EDITOR
    [Title("Preview"), PropertyOrder(100)]
    [SerializeField, PreviewField(Height = 100)]
    private Sprite previewSprite;
    
    [PropertyOrder(101)]
    [ValueDropdown("GetAnimationNames")]
    public string previewAnimation;

    private IEnumerable GetAnimationNames()
    {
        return animations.ConvertAll(a => a.name);
    }

    [Button("Preview Previous Frame"), PropertyOrder(102)]
    private void PreviewPreviousFrame()
    {
        var animation = animations.Find(a => a.name == previewAnimation);
        if (animation == null || animation.frames.Count == 0) return;
        
        currentFrame = (currentFrame - 1 + animation.frames.Count) % animation.frames.Count;
        previewSprite = animation.frames[currentFrame];
        if (!Application.isPlaying && spriteRenderer != null)
        {
            spriteRenderer.sprite = previewSprite;
        }
    }

    [Button("Preview Next Frame"), PropertyOrder(102)]
    private void PreviewNextFrame()
    {
        var animation = animations.Find(a => a.name == previewAnimation);
        if (animation == null || animation.frames.Count == 0) return;
        
        currentFrame = (currentFrame + 1) % animation.frames.Count;
        previewSprite = animation.frames[currentFrame];
        if (!Application.isPlaying && spriteRenderer != null)
        {
            spriteRenderer.sprite = previewSprite;
        }
    }

    private float lastEditorTime;
    private bool isPreviewPlaying = false;

    [Button("Play Preview"), PropertyOrder(103)]
    private void PlayPreview()
    {
        if (Application.isPlaying) return;
        StopPreview();
        isPreviewPlaying = true;
        lastEditorTime = Time.realtimeSinceStartup;
        UnityEditor.EditorApplication.update += EditorUpdate;
    }

    [Button("Stop Preview"), PropertyOrder(103)]
    private void StopPreview()
    {
        if (Application.isPlaying) return;
        isPreviewPlaying = false;
        UnityEditor.EditorApplication.update -= EditorUpdate;
    }

    private void EditorUpdate()
    {
        if (!isPreviewPlaying) return;
        
        var animation = animations.Find(a => a.name == previewAnimation);
        if (animation == null || animation.frames.Count == 0) return;

        float deltaTime = Time.realtimeSinceStartup - lastEditorTime;
        frameTimer += deltaTime;

        if (frameTimer >= 1f / animation.fps)
        {
            frameTimer = 0f;
            PreviewNextFrame();
            UnityEditor.SceneView.RepaintAll();
        }

        lastEditorTime = Time.realtimeSinceStartup;
    }
    #endif
}
