using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_TimeManager : KS_Singleton<KS_TimeManager>
    {
        public static float TimeScale = 1;

        public static float TimeSinceGameStart = 0;

        private static bool _IsPaused = false;

        static Dictionary<object, float> _TimeScaleOverrides = new Dictionary<object, float>();
        static float _PauseTimeBackup = 1;

        private void Awake()
        {
            TimeSinceGameStart = 0;
            Time.timeScale = 1;
        }

        public static void SetPauseState(bool paused)
        {
            _IsPaused = paused;

            if (paused)
            {
                _PauseTimeBackup = TimeScale;
                TimeScale = 0;
                Time.timeScale = 0;
                KS_Logger.Log("TimeManager: Time paused");
            }
            else
            {
                TimeScale = _PauseTimeBackup;
                Time.timeScale = _PauseTimeBackup;
                KS_Logger.Log("TimeManager: Time resumed");
            }
        }

        public static void SetTimeScaleOverride(object key, float timeScale)
        {
            if (_TimeScaleOverrides.ContainsKey(key))
            {
                TimeScale /= _TimeScaleOverrides[key];
                _TimeScaleOverrides[key] = timeScale;
            }
            else
                _TimeScaleOverrides.Add(key, timeScale);

            UpdateTime();
        }

        public static void RemoveTimeScaleOverride(object key)
        {
            if (_TimeScaleOverrides.ContainsKey(key))
            {
                _TimeScaleOverrides.Remove(key);
            }
            
            else
                Debug.LogError("TimeScale override not found for key: " + key);

            UpdateTime();
        }


        public static void UpdateTime()
        {
            float newTimeScale = 1;

            foreach (var timeScaleOverride in _TimeScaleOverrides)
            {
                newTimeScale *= timeScaleOverride.Value;
            }

            Time.timeScale = newTimeScale;
        }


        private void OnDestroy() {
            _TimeScaleOverrides.Clear();
            Time.timeScale = 1;
        }
    }

}