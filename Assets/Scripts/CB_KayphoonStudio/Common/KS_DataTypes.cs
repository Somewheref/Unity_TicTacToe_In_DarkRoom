using System.Collections;
using System.Collections.Generic;
using KayphoonStudio.SimpleHelper;
using UnityEngine;


namespace KayphoonStudio
{
    public class KS_DataTypes
    {
        [System.Serializable]
        public class MinMaxRange
        {
            public float min;
            public float max;

            public MinMaxRange(float min, float max)
            {
                this.min = min;
                this.max = max;
            }

            public float RandomValue()
            {
                return KS_Random.Range(min, max);
            }
        }


        public enum FilePathType
        {
            PersistentDataPath,
            StreamingAssetsPath,
            TemporaryCachePath,
            DataPath,
            CustomPath
        }
    }


    /// <summary>
    /// KS_StringList myStringList = new KS_StringList("myListKey");
    ///    myStringList.Add("Apple");
    ///    myStringList.Add("Banana");
    ///    myStringList.Remove("Apple");
    ///    bool containsBanana = myStringList.Contains("Banana");
    ///    Debug.Log("Item count: " + myStringList.Count());

    /// </summary>
    public class KS_SavedStringList
    {
        private string _key;
        private char _separator = '·';  // You can change this if needed
        private List<string> _list;

        // Constructor to initialize the class
        public KS_SavedStringList(string key, char separator = '·')
        {
            _key = key;
            _separator = separator;
            _list = LoadList();
        }

        // Add an item to the list
        public void Add(string item)
        {
            if (!_list.Contains(item))
            {
                _list.Add(item);
                SaveList();
            }
        }

        // Remove an item from the list
        public void Remove(string item)
        {
            if (_list.Contains(item))
            {
                _list.Remove(item);
                SaveList();
            }
        }

        // Check if the list contains the item
        public bool Contains(string item)
        {
            return _list.Contains(item);
        }

        // Clear the entire list
        public void Clear()
        {
            _list.Clear();
            SaveList();
        }

        // Get the list as a string array
        public string[] ToArray()
        {
            return _list.ToArray();
        }

        public List<string> ToList()
        {
            return _list;
        }

        // Return the count of items in the list
        public int Count()
        {
            return _list.Count;
        }

        // Save the list to PlayerPrefs
        private void SaveList()
        {
            string joinedString = string.Join(_separator.ToString(), _list);
            KS_SimpleSaver.Save(_key, joinedString);
            //KS_Logger.Log("Saved list: " + joinedString);
        }

        // Load the list from PlayerPrefs
        private List<string> LoadList()
        {
            string savedString = KS_SimpleSaver.Load<string>(_key, string.Empty);
            if (string.IsNullOrEmpty(savedString))
                return new List<string>();
            
            return new List<string>(savedString.Split(_separator));
        }

        // Access list elements by index
        public string this[int index]
        {
            get { return _list[index]; }
            set
            {
                _list[index] = value;
                SaveList();
            }
        }

        // *** NEW: Add a JSON object to the list ***
        public void AddJson<T>(T item) where T : class
        {
            string json = JsonUtility.ToJson(item);
            Add(json);  // Save the JSON string to the list
        }

        public void RemoveJson<T>(T item) where T : class
        {
            string json = JsonUtility.ToJson(item);
            Remove(json);  // Save the JSON string to the list
        }

        // *** NEW: Get JSON object list as List<T> ***
        public List<T> GetJsonList<T>() where T : class
        {
            List<T> objectList = new List<T>();
            foreach (string json in _list)
            {
                T obj = JsonUtility.FromJson<T>(json);
                objectList.Add(obj);
            }
            return objectList;
        }

        // *** NEW: Replace the entire list with a list of JSON objects ***
        public void SetJsonList<T>(List<T> objectList) where T : class
        {
            _list.Clear();
            foreach (T obj in objectList)
            {
                AddJson(obj);
            }
            SaveList();
        }
    }
}