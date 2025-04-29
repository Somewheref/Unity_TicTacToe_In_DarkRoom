using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class KS_BuiltInExtensions
{

    public static void KS_ChangeLayerRecursively(this GameObject obj, string name)
    {
        obj.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in obj.transform)
        {
            if (child == null)
                continue;
            child.gameObject.KS_ChangeLayerRecursively(name);
        }
    }

    public static void KS_ChangeLayerRecursively(this GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            if (child == null)
                continue;
            child.gameObject.KS_ChangeLayerRecursively(layer);
        }
    }


    public static void KS_ChangeTagRecursively(this GameObject obj, string tag)
    {
        obj.tag = tag;
        foreach (Transform child in obj.transform)
        {
            if (child == null)
                continue;
            child.gameObject.KS_ChangeTagRecursively(tag);
        }
    }

    public static void KS_GroupSetActive(this GameObject[] gameObjects, bool active)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(active);
        }
    }

    /// <summary>
    /// Set the alpha of canvas group to 0, and set Interactable, BlocksRaycasts to false
    /// </summary>
    public static void KS_Hide(this CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public static void KS_HideWithoutChangeAlpha(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Set the alpha of canvas group to 1, and set Interactable, BlocksRaycasts to true
    /// </summary>
    public static void KS_Show(this CanvasGroup canvasGroup, bool blockRaycast = true)
    {
        if (blockRaycast)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        canvasGroup.alpha = 1;
    }

    public static void KS_ShowWithoutChangeAlpha(this CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Changes the parent transform of the given transform, while keeping the world position, rotation, and scale of each child.
    /// Note: the function might not handle uneven scaling correctly.
    /// </summary>
    public static void KS_ChangeTransformKeepChildren(this Transform parentTransform, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        // First, we create a dictionary to store the world position, rotation, and scale of each child
        Dictionary<Transform, (Vector3 position, Quaternion rotation, Vector3 scale)> childrenTransforms =
            new Dictionary<Transform, (Vector3 position, Quaternion rotation, Vector3 scale)>();

        foreach (Transform child in parentTransform)
        {
            childrenTransforms[child] = (child.position, child.rotation, child.lossyScale);
        }

        // Now we can change the parent's transform
        parentTransform.position = position;
        parentTransform.rotation = rotation;
        parentTransform.localScale = scale;

        // And finally, restore each child's world transform
        foreach (KeyValuePair<Transform, (Vector3 position, Quaternion rotation, Vector3 scale)> pair in childrenTransforms)
        {
            pair.Key.position = pair.Value.position;
            pair.Key.rotation = pair.Value.rotation;
            pair.Key.localScale = pair.Value.scale;
        }
    }


    public static void KS_ChangePositionKeepChildren(this Transform parentTransform, Vector3 position)
    {
        KS_ChangeTransformKeepChildren(parentTransform, position, parentTransform.rotation, parentTransform.localScale);
    }


    public static void KS_SetActiveChildren(this Transform parentTransform, bool active)
    {
        foreach (Transform child in parentTransform)
        {
            child.gameObject.SetActive(active);
        }
    }
    
}