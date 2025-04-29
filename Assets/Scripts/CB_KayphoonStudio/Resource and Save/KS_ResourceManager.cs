using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace KayphoonStudio
{
    public class KS_ResourceManager : MonoBehaviour
    {
        static Dictionary<object, List<object>> resources = new Dictionary<object, List<object>>();


        public static T GetResource<T>(string path, bool cacheResource = true)
        {
            if (GetResourceFromCache(path, out object item))
            {
                return (T)item;
            }

            else
            {
                item = LoadResource(path);
                if (cacheResource)
                {
                    AddResource(path, item);
                }

                return (T)item;
            }
        }
        

        public static T[] GetResourceFolder<T>(string path, bool cacheResource = true, bool includeSubFolders = true)
        {
            if (GetResourceFolderFromCache(path, out object[] items))
            {
                return ConvertType<T>(items);
            }

            else
            {
                items = LoadResourceFolder(path, includeSubFolders);
                if (cacheResource)
                {
                    foreach (var item in items)
                    {
                        AddResource(path, item);
                    }
                }

                if (items == null)
                {
                    KS_Logger.LogError("No items found in folder " + path, typeof(KS_ResourceManager));
                }

                return ConvertType<T>(items);
            }
        }

        /// <summary>
        /// Load .txt file from streaming assets folder <br/>
        /// Currently supports: Android
        /// Not testedd: iOS, Windows, Mac, others
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadStreamingAssetTXT(string path, Encoding encoding = null)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            string textData = string.Empty;

            encoding ??= Encoding.Default;

            # if UNITY_ANDROID && !UNITY_EDITOR
            WWW reader = new WWW(filePath);
            while (!reader.isDone) {
            }
            textData = reader.text;
            # else
            textData = File.ReadAllText(filePath);
            # endif

            return textData;
        }

        /// <summary>
        /// Load .txt file from Resource folder <br/>
        /// Currently supports: Android
        /// Not testedd: iOS, Windows, Mac, others
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadResourceTXT(string path, Encoding encoding = null)
        {
            // remove externsion    
            path = path.Replace(".txt", "");
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string textData = textAsset.text;
            //Debug.Log(textData);
            return textData;
        }


        public static void UnloadResource(string path)
        {
            if (!resources.ContainsKey(path)) return;

            foreach (var item in resources[path])
            {
                Resources.UnloadAsset(item as UnityEngine.Object);
            }

            resources.Remove(path);
        }



        # region internal methods


        protected static object LoadResource(string path)
        {
            object item = Resources.Load(path);
            return item;
        }

        protected static object[] LoadResourceFolder(string path, bool includeSubFolders = true)
        {
            if (includeSubFolders)
            {
                object[] items = Resources.LoadAll(path);
                return items;
            }
            
            else
            {
                List<object> items = new List<object>();
                foreach (var item in Resources.LoadAll(path))
                {
                    if (item.GetType() == typeof(GameObject))
                    {
                        items.Add(item);
                    }
                }

                return items.ToArray();
            }
        }

        protected static bool GetResourceFromCache(string path, out object item)
        {
            if (resources.ContainsKey(path))
            {
                item = resources[path][0];
                return true;
            }

            item = null;
            return false;
        }

        protected static bool GetResourceFolderFromCache(string path, out object[] items)
        {
            if (resources.ContainsKey(path))
            {
                items = resources[path].ToArray();
                return true;
            }
            else
            {
                items = null;
                return false;
            }
        }

        protected static void AddResource(object key, object item)
        {
            if (!resources.ContainsKey(key))
            {
                resources.Add(key, new List<object>());
            }

            resources[key].Add(item);
        }


        protected static T[] ConvertType<T>(object[] items)
        {
            List<T> convertedItems = new List<T>();
            foreach (var item in items)
            {
                convertedItems.Add((T)item);
            }

            return convertedItems.ToArray();
        }


        # endregion
    }

}