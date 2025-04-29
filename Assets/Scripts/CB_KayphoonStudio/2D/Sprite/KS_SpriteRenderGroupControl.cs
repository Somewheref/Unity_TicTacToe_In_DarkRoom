using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace KayphoonStudio._2D.Sprite
{
    public class KS_SpriteRenderGroupControl : MonoBehaviour
    {
        public SpriteRenderer[] spriteRenderers;

        public bool controlColor = false;
        [ShowIf("controlColor")]
        public Color color = Color.white;

        public bool InitOnStart = true;


        private Color[] originalColors;


        [Button("Test GroupControl")]
        public void TestGroupControl()
        {
            originalColors = new Color[spriteRenderers.Length];
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                originalColors[i] = spriteRenderers[i].color;
            }
            GroupControl();
        }

        [Button("Reset Control")]
        public void ResetControls()
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                if (controlColor)
                {
                    spriteRenderers[i].color = originalColors[i];
                }
            }
        }

        private void Start()
        {
            if (InitOnStart)
            {
                GroupControl();
            }
        }

        public void GroupControl()
        {
            foreach (SpriteRenderer sr in spriteRenderers)
            {
                if (controlColor)
                {
                    sr.color = color;
                }
            }
        }
    }
}
