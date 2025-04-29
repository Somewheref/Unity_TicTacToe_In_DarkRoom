using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_Logger : MonoBehaviour
    {
        public static void DebugLog<T>(T message)
        {
            Debug.Log(message);
        }
        public static void Log(string message, string color = "white")
        {
            Debug.Log("<color=" + color + ">" + message + "</color>");
        }
        public static void Log(string message, object sender, string color = "white")
        {
            Debug.Log("<color=" + color + ">" + sender + ": " +  message + "</color>");
        }

        public static void LogWarning(string message, object sender, string color = "yellow")
        {
            Debug.LogWarning("<color=" + color + ">" + message + "</color>");
        }

        public static void LogError(string message, object sender = null, string color = "red")
        {
            Debug.LogError("<color=" + color + ">" + message + "</color>");
        }
    }

    public static class KS_Builds_LogDisabler
    {
        /// <summary>
        /// Gain performance on regular code execution outside of editor by disabling Debug logger.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void DisableLoggerOutsideOfEditor()
        {
            #if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.unityLogger.logEnabled = true;
            #else
                Debug.unityLogger.logEnabled = false;
            #endif
        }
    }
}
