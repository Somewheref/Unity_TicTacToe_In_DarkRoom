using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_SaverBase : MonoBehaviour
    {
        
        public static void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static void Increment(string key)
        {
            Save(key, Load<int>(key,0)+1);
        }

        public static void Save(string key, int value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        public static void Save(string key, float value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        public static void Save(string key, bool value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static T Load<T>(string key, T defaultValue = default(T))
        {
            if (PlayerPrefs.HasKey(key))
            {
                return (T)System.Convert.ChangeType(PlayerPrefs.GetString(key), typeof(T));
            }
            else
            {
                //KS_Logger.LogError("No value found for key " + key, typeof(KS_SimpleSaver));
                return defaultValue;
            }
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static string[] CantRemoveSaveKeys = { };

        public static void ForceClearSave()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void ClearSave()
        {
            // 1. ������Ҫ����������
            Dictionary<string, string> backupStringData = new Dictionary<string, string>();
            Dictionary<string, int> backupIntData = new Dictionary<string, int>();
            Dictionary<string, float> backupFloatData = new Dictionary<string, float>();

            foreach (string key in CantRemoveSaveKeys)
            {
                if (PlayerPrefs.HasKey(key))
                {
                    // �����Բ�ͬ�����Ͷ�ȡ����
                    // ע��: �������û�м����Զ������ʹ洢��PlayerPrefs��
                    if (float.TryParse(PlayerPrefs.GetString(key, ""), out float floatResult))
                    {
                        backupFloatData[key] = PlayerPrefs.GetFloat(key);
                    }
                    else if (int.TryParse(PlayerPrefs.GetString(key, ""), out int intResult))
                    {
                        backupIntData[key] = PlayerPrefs.GetInt(key);
                    }
                    else
                    {
                        backupStringData[key] = PlayerPrefs.GetString(key);
                    }
                }
            }

            // 2. ɾ����������
            PlayerPrefs.DeleteAll();

            // 3. �ָ����ݵ�����
            foreach (var kvp in backupStringData)
            {
                Save(kvp.Key, kvp.Value);
            }
            foreach (var kvp in backupIntData)
            {
                Save(kvp.Key, kvp.Value);
            }
            foreach (var kvp in backupFloatData)
            {
                Save(kvp.Key, kvp.Value);
            }

            // �������
            PlayerPrefs.Save();
        }
    }
}
