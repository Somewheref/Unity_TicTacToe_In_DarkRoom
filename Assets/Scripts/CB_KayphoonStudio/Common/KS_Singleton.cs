using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>(true);

                if (_instance == null)
                {
                    //Debug.LogError("Cannot find " + typeof(T) + "!");
                }
            }

            return _instance;
        }
    }
}
