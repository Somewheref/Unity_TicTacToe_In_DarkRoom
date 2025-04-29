using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    // ���ص� GetChildCollection ���������б�ǩ������Ĭ��ʹ�� "parent" ��ǩ
    public static List<GameObject> GetChildCollection(this GameObject parentObject, string tag = "")
    {
        List<GameObject> childCollection = new List<GameObject>();

        Transform parentTransform = parentObject.transform;
        int childCount = parentTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentTransform.GetChild(i);
            GameObject childGameObject = childTransform.gameObject;

            // ����Ӷ���ı�ǩ�Ƿ�ƥ�䴫��ı�ǩ
            if (childGameObject.CompareTag(tag) || tag=="")
            {
                childCollection.Add(childGameObject);
            }
        }

        return childCollection;
    }
}


namespace KayphoonStudio
{

    public class KS_Color
    {
        public static Color Transparent_Cyan = new Color(0, 1, 1, 0.5f);
    }


    public class KS_Delayer
    {
        public static IEnumerator Delay(float delayTime, System.Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action();
        }
    }


    public enum UpdateType
    {
        Update,
        FixedUpdate,
        Coroutine
    }


    public class Utils
    {
        public static LayerMask NameToLayer(string layerName)
        {
            return 1 << LayerMask.NameToLayer(layerName);
        }

        public static LayerMask NamesToLayer(string[] layerName)
        {
            LayerMask mask = 0;
            foreach (string name in layerName)
            {
                mask |= (1 << LayerMask.NameToLayer(name));
            }
            return mask;
        }

        public static string LayerToName(LayerMask layer)
        {
            return LayerMask.LayerToName(layer);
        }


        public static void ChangeLayerRecursively(GameObject obj, string name)
        {
            obj.layer = LayerMask.NameToLayer(name);
            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;
                ChangeLayerRecursively(child.gameObject, name);
            }
        }

        public static void ChangeLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;
                ChangeLayerRecursively(child.gameObject, layer);
            }
        }


        public static void ChangeTagRecursively(GameObject obj, string tag)
        {
            obj.tag = tag;
            foreach (Transform child in obj.transform)
            {
                if (child == null)
                    continue;
                ChangeTagRecursively(child.gameObject, tag);
            }
        }

        public static List<GameObject> KS_FindGameObjectsWithTagIncludeInactive(string tag)
        {
            GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>(true);
            List<GameObject> gameObjectsWithTag = new List<GameObject>();
            foreach (var gameObject in allGameObjects)
            {
                if (gameObject.CompareTag(tag))
                {
                    gameObjectsWithTag.Add(gameObject);
                }
            }

            return gameObjectsWithTag;
        }


        public static Transform FindChildRecursively(Transform parent, string name)
        {
            if (parent.name == name)
            {
                return parent;
            }
            foreach (Transform child in parent.transform)
            {
                Transform result = FindChildRecursively(child.transform, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }


        public static T GetComponentSelfOrChild<T>(GameObject obj) where T : Component
        {
            T result = obj.GetComponent<T>();
            if (result == null)
            {
                result = obj.GetComponentInChildren<T>();
            }
            return result;
        }

        public static T[] GetComponentsSelfOrChild<T>(GameObject obj) where T : Component
        {
            List<T> result = new List<T>();
            result.AddRange(obj.GetComponents<T>());
            result.AddRange(obj.GetComponentsInChildren<T>());
            return result.ToArray();
        }


        public static bool TryGetComponentSelfOrParent<T>(GameObject obj, out T result) where T : Component
        {
            result = obj.GetComponent<T>();
            if (result == null)
            {
                result = obj.GetComponentInParent<T>();
            }
            return result != null;
        }


        public static void Destroy(GameObject obj)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(obj);
            }
            else
            {
                GameObject.DestroyImmediate(obj);
            }
        }

        public static void DestroyAllChildren(Transform parent, bool ignoreInactiveChild = false)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                if (ignoreInactiveChild && !parent.GetChild(i).gameObject.activeSelf)
                    continue;
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        public static GameObject[] GetChildRecursively(GameObject parent)
        {
            List<GameObject> result = new List<GameObject>();
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                result.Add(parent.transform.GetChild(i).gameObject);
                result.AddRange(GetChildRecursively(parent.transform.GetChild(i).gameObject));
            }
            return result.ToArray();
        }

        public static string FormatDistance(float distance, int decimalPlaces = 1)
        {
            if (distance < 1000)
            {
                return distance.ToString("F0") + "m";
            }
            else
            {
                //return (distance / 1000).ToString("F1") + "km";
                return (distance / 1000).ToString("F" + decimalPlaces) + "km";
            }
        }
    }

}