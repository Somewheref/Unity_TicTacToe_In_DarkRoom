using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class MetaToolkit : MonoBehaviour
{

    public static Action OnWindowsLostFocus;
    public static Action OnWindowsGainedFocus;

    public static string GetWindowsUsername()
    {
        #if UNITY_STANDALONE_WIN
        string username = System.Environment.UserName;
        Debug.Log("Windows Username: " + username);
        return username;
        #else
        Debug.Log("Not running on Windows, username unavailable.");
        return "UnknownUser";
        #endif
    }

    private bool _wasFocused = true;

    /// <summary>
    /// IMPORTANT: To use this function, attach this monobehaviour to a gameobject in the scene.
    /// </summary>
    /// <param name="hasFocus"></param>
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Debug.Log("Lost focus on Windows application");
            OnWindowsLostFocus?.Invoke();
            _wasFocused = false;
        }
        else
        {
            if (!_wasFocused)
            {
                Debug.Log("Regained focus on Windows application"); 
                OnWindowsGainedFocus?.Invoke();
            }
            _wasFocused = true;
        }
    }
}
