using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KayphoonStudio.EditorHelpers
{
    /// <summary>
    /// This script is used to reset the transform of the object it is attached to, while making sure
    /// all of its children keep their local position, rotation and scale.
    /// </summary>
    public class KS_TransformResetter : MonoBehaviour
    {
        [InfoBox("This script is used to reset the transform of the object it is attached to, while making sure all of its children keep their local position, rotation and scale.")]
        [Button("Reset Transform", ButtonSizes.Large)]
        public void ResetTransform()
        {
            // get first level children
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
                children.Add(transform.GetChild(i));
            
            // create a temporary parent
            GameObject tempParent = new GameObject("Temp Parent");

            // move all children to the temporary parent
            foreach (Transform child in children)
                child.SetParent(tempParent.transform);
            
            // reset the transform of the object this script is attached to
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            // move all children back to the object this script is attached to
            foreach (Transform child in children)
                child.SetParent(transform);
            
            // destroy the temporary parent
            DestroyImmediate(tempParent);
        }
    }

}