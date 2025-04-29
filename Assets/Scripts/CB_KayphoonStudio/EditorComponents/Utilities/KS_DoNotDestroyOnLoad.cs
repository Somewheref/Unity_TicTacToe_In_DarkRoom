using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using KayphoonStudio;

public class KS_DoNotDestroyOnLoad : MonoBehaviour
{
    public GameObject[] ObjectsToNotDestroy;
    [Tooltip("If true, the objects will be moved to the root of the scene hierarchy.")]
    public bool AutoMoveToRoot = true;
    public bool DestroySelfIfAlreadyExist = true;
    public string uniqueName = "KS_DoNotDestroyOnLoad_ID";

    private static HashSet<string> uniqueNames = new HashSet<string>();

    private void Reset()
    {
        ObjectsToNotDestroy = new GameObject[1];
        ObjectsToNotDestroy[0] = gameObject;

        // generate unique name
        uniqueName = Random.Range(0, 100000000).ToString();
    }

    void Awake()
    {
        // check if already exist
        KS_DoNotDestroyOnLoad[] doNotDestroyOnLoads = FindObjectsOfType<KS_DoNotDestroyOnLoad>();
        foreach (KS_DoNotDestroyOnLoad doNotDestroyOnLoad in doNotDestroyOnLoads)
        {
            if (doNotDestroyOnLoad.uniqueName == uniqueName && doNotDestroyOnLoad != this)
            {
                if (DestroySelfIfAlreadyExist)
                {
                    KS_Logger.LogWarning("Destroying duplicate KS_DoNotDestroyOnLoad on " + gameObject.name, this);
                    Destroy(gameObject);
                }
                return;
            }
        }

        if (!uniqueNames.Add(uniqueName))
        {
            if (DestroySelfIfAlreadyExist)
            {
                KS_Logger.LogWarning("Destroying duplicate KS_DoNotDestroyOnLoad on " + gameObject.name, this);
                Destroy(gameObject);
            }
            return;
        }

        // move to root
        if (AutoMoveToRoot)
        {
            foreach (GameObject go in ObjectsToNotDestroy)
            {
                go.transform.SetParent(null);
            }
        }
        foreach (GameObject go in ObjectsToNotDestroy)
        {
            DontDestroyOnLoad(go);
        }
    }
}
