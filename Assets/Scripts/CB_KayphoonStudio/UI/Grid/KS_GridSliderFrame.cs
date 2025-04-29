using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace KayphoonStudio.UI
{
    /// <summary>
    /// Create a number of frames for a grid slider, also scale the fill image to fit the frame.
    /// the frames will be placed under a grid layout group, so we simply need to instantiate the frame prefab and set the fill image to the correct size.
    /// </summary>
    public class KS_GridSliderFrame : MonoBehaviour
    {
        public GameObject framePrefab;
        public RectTransform fill;
        [field: SerializeField] public int frameCount { get; protected set; } = 10;
        public Vector2 fillScaleMultiplier = Vector2.one;
        public bool initOnStart = false;
        public bool useFirstChildAsFramePrefab = false;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void InitFrameCount(int count)
        {
            frameCount = count;
            InitFrames();
        }

        [Button("Init Frames")]
        protected void InitFrames()
        {
            Vector2 fillScale = fill.localScale;

            fill.localScale = new Vector3(fillScale.x * fillScaleMultiplier.x * frameCount, fillScale.y * fillScaleMultiplier.y, 1);

            for (int i = 0; i < (useFirstChildAsFramePrefab ? frameCount - 1 : frameCount); i++)
            {
                GameObject frame = Instantiate(framePrefab, transform);
                frame.name = "Frame " + i;
            }
        }
    }

}