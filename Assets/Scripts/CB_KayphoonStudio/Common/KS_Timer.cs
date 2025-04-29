using System.Collections;
using UnityEngine;
using System;

namespace KayphoonStudio
{
    public class KS_Timer : MonoBehaviour
    {
        private static KS_Timer instance;

        public static KS_Timer Instance
        {
            get
            {
                if (instance == null)
                {
                    // Create new GameObject with KS_Timer
                    GameObject ksTimer = new GameObject("KS_Timer");
                    instance = ksTimer.AddComponent<KS_Timer>();
                    // Make sure it won't be destroyed
                    DontDestroyOnLoad(ksTimer);
                }
                return instance;
            }
        }

        public static void DelayedAction(float delay, Action action)
        {
            Instance.StartCoroutine(Instance.DelayedActionCoroutine(action, delay));
        }

        public static void DelayedEndOfFrameAction(Action action)
        {
            Instance.StartCoroutine(Instance.DelayedEndOfFrameActionCoroutine(action));
        }

        /// <summary>
        /// Example Usage: <br/>
        /// <code>
        /// KS_Timer.DelayedPropertySet(val => myFloat = val, 5.0f, 2.0f);
        /// </code>
        /// </summary>
        public static void DelayedPropertySet<T>(Action<T> setAction, T value, float delay)
        {
            Instance.StartCoroutine(Instance.DelayedActionCoroutine(() => setAction(value), delay));
        }


        public static void WaitForCondition(Func<bool> condition, Action action, float checkInterval = 0.1f)
        {
            Instance.StartCoroutine(Instance.WaitForConditionCoroutine(condition, action, checkInterval));
        }


        /// <summary>
        /// Example Usage: <br/>
        /// <code>
        /// KS_Timer.InvokeRepeating(() => myFloat += 1.0f, 1.0f, 5, 2.0f);
        /// </code>
        /// </summary>
        public static void InvokeRepeating(Action action, float repeatInterval, int repeatCount = -1, float delay = 0f)
        {
            Instance.StartCoroutine(Instance.InvokeRepeatingCoroutine(action, repeatInterval, repeatCount, delay));
        }


        IEnumerator WaitForConditionCoroutine(Func<bool> condition, Action action, float checkInterval)
        {
            while (!condition())
            {
                yield return new WaitForSeconds(checkInterval);
            }
            tryAction(action);
        }


        IEnumerator DelayedEndOfFrameActionCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            tryAction(action);
        }


        IEnumerator DelayedActionCoroutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            tryAction(action);
        }
    

        IEnumerator InvokeRepeatingCoroutine(Action action, float repeatInterval, int repeatCount = -1, float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            if (repeatCount == -1)
            {
                while (true)
                {
                    tryAction(action);
                    yield return new WaitForSeconds(repeatInterval);
                }
            }
            else
            {
                for (int i = 0; i < repeatCount; i++)
                {
                    tryAction(action);
                    yield return new WaitForSeconds(repeatInterval);
                }
            }
        }

        bool tryAction(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception e)
            {
                KS_Logger.LogError(e.ToString());
                return false;
            }
        }
    
    }
}
