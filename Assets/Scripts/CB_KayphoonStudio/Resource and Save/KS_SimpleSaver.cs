using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if EASY_SAVE
using ES3Internal;
# endif

namespace KayphoonStudio.SimpleHelper
{
    public static class KS_SimpleSaver
    {
        public enum SaveMethod
        {
            PlayerPrefs,
            # if EASY_SAVE
            EasySave
            #endif
        }

        // NOTE: change here for your preferred saving type
        # if EASY_SAVE
        public static SaveMethod saveMethod = SaveMethod.EasySave;
        #else
        public static SaveMethod saveMethod = SaveMethod.PlayerPrefs;
        #endif

        # region Core Save / Load Methods
        public static void Save(string key, object value)
        {
            switch (saveMethod)
            {
                case SaveMethod.PlayerPrefs:
                    switch (value)
                    {
                        case string stringValue:
                            PlayerPrefs.SetString(key, stringValue);
                            break;
                        case int intValue:
                            PlayerPrefs.SetString(key, intValue.ToString());
                            break;
                        case float floatValue:
                            PlayerPrefs.SetString(key, floatValue.ToString());
                            break;
                        case bool boolValue:
                            PlayerPrefs.SetString(key, boolValue.ToString());
                            break;
                        default:
                            try
                            {
                                KS_Logger.LogError("Unsupported type for saving in method PlayerPref: " + value.GetType(), typeof(KS_SimpleSaver));
                            }
                            catch (System.Exception e)
                            {
                                KS_Logger.LogError("Unsupported type for saving in method PlayerPref, and is having trouble logging the type: " + e, typeof(KS_SimpleSaver));
                            }
                            
                            break;
                    }
                    break;
                # if EASY_SAVE
                case SaveMethod.EasySave:
                    // ES3.Save(key, value);

                    switch (value)
                    {
                        case string stringValue:
                            ES3.Save<string>(key, stringValue);
                            break;
                        case int intValue:
                            ES3.Save<int>(key, intValue);
                            break;
                        case float floatValue:
                            ES3.Save<float>(key, floatValue);
                            break;
                        case bool boolValue:
                            ES3.Save<bool>(key, boolValue);
                            break;
                        default:
                            ES3.Save(key, value);
                            break;
                    }
                    break;
                #endif
            }
        }


        public static T Load<T>(string key, T defaultValue = default(T))
        {
            switch (saveMethod)
            {
                case SaveMethod.PlayerPrefs:
                    if (PlayerPrefs.HasKey(key))
                    {
                        return (T)System.Convert.ChangeType(PlayerPrefs.GetString(key), typeof(T));
                    }
                    else
                    {
                        return defaultValue;
                    }
                # if EASY_SAVE
                case SaveMethod.EasySave:
                    if (ES3.KeyExists(key))
                    {
                        //return ES3.Load<T>(key);
                        var data = ES3.Load(key);
                        if (data is T)
                        {
                            return (T)data;
                        }
                        else
                        {
                            // try convert data to T
                            try
                            {
                                return (T)System.Convert.ChangeType(data, typeof(T));
                            }
                            catch (System.Exception e)
                            {
                                KS_Logger.LogError("Failed to convert data to type " + typeof(T) + " for key " + key + " with data " + data + " : " + e, typeof(KS_SimpleSaver));
                                return defaultValue;
                            }
                        }
                    }
                    else
                    {
                        return defaultValue;
                    }
                #endif
                default:
                    return defaultValue;
            }
        }

        public static bool HasKey(string key)
        {
            switch (saveMethod)
            {
                case SaveMethod.PlayerPrefs:
                    return PlayerPrefs.HasKey(key);
                # if EASY_SAVE
                case SaveMethod.EasySave:
                    return ES3.KeyExists(key);
                #endif
                default:
                    return false;
            }
        }


        public static void DeleteKey(string key)
        {
            switch (saveMethod)
            {
                case SaveMethod.PlayerPrefs:
                    PlayerPrefs.DeleteKey(key);
                    break;
                # if EASY_SAVE
                case SaveMethod.EasySave:
                    ES3.DeleteKey(key);
                    break;
                #endif
            }
        }

        public static void DeleteAll()
        {
            switch (saveMethod)
            {
                case SaveMethod.PlayerPrefs:
                    PlayerPrefs.DeleteAll();
                    break;
                # if EASY_SAVE
                case SaveMethod.EasySave:
                    ES3.DeleteFile();   // TODO: might be multiple files if project is complicated
                    break;
                #endif
            }
        }

        # endregion


        public static void Increment(string key)
        {
            Save(key, Load<int>(key,0)+1);
        }

        public static string[] CantRemoveSaveKeys = { };

        public static void ForceClearSave()
        {
            DeleteAll();
        }

        public static void ClearSave()
        {
            // 1. ������Ҫ����������
            Dictionary<string, string> backupStringData = new Dictionary<string, string>();
            Dictionary<string, int> backupIntData = new Dictionary<string, int>();
            Dictionary<string, float> backupFloatData = new Dictionary<string, float>();

            foreach (string key in CantRemoveSaveKeys)
            {
                if (HasKey(key))
                {
                    // �����Բ�ͬ�����Ͷ�ȡ����
                    // ע��: �������û�м����Զ������ʹ洢��PlayerPrefs��
                    if (float.TryParse(Load(key, ""), out float floatResult))
                    {
                        backupFloatData[key] = Load<float>(key);
                    }
                    else if (int.TryParse(Load(key, ""), out int intResult))
                    {
                        backupIntData[key] = Load<int>(key);
                    }
                    else
                    {
                        backupStringData[key] = Load<string>(key);
                    }
                }
            }

            // 2. ɾ����������
            DeleteAll();

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