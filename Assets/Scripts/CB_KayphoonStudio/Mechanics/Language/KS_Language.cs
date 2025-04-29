using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using KayphoonStudio;
using TMPro;
using System.Linq;
using Sirenix.OdinInspector;

namespace KayphoonStudio.Language
    {
    public class KS_Language : KS_Singleton<KS_Language>
    {

        Dictionary<string, string> LanguageDictionary = new Dictionary<string, string>();
        public bool ForcedSetLang = false;
        public string CurrentLang;
        private string LangKey = "lang";
        public static string DefaultLanguage = "ch";
        public List<LanguageData> LanguageList=new List<LanguageData>();
        public LanguageData CurrentLanguage;

        // added by Stanly: a list of reference to enhance performance
        public bool useCachedReferences = true;
        public List<TMP_Text> TextsInScene = new List<TMP_Text>();
        public List<KS_LanguageImageTranslate> ImageTranslates = new List<KS_LanguageImageTranslate>();

        public bool LogErrorInfo = true;

        public AdvancedOptions advancedOptions = new AdvancedOptions();

        [System.Serializable]
        public class AdvancedOptions
        {
            [Tooltip("If true, the system will exclude inactive objects from the search (for performance reasons if there are a lot of objects in the scene)")]
            public bool ExcludeInactive = false;
            [Tooltip("If true, we will only translate objects with tag UIText")]
            public bool OnlyUITextTag = false;
            [Tooltip("If true, all fonts in game will be set to current language regardless if they are in the dictionary or not")]
            public bool ChangeAllFonts = true;
        }


        [ContextMenu("Add All TMP to List")]
        public void AddAllTMPToList()
        {
            TextsInScene.Clear();
            TextsInScene = GetAllTranslateObjects();
        }


        List<TMP_Text> GetAllTranslateObjects()
        {
            if (advancedOptions.OnlyUITextTag)
            {
                if (advancedOptions.ExcludeInactive)
                {
                    return GameObject.FindGameObjectsWithTag("UIText").ToList().ConvertAll(x => x.GetComponent<TMP_Text>());
                }
                else
                    return Utils.KS_FindGameObjectsWithTagIncludeInactive("UIText").ConvertAll(x => x.GetComponent<TMP_Text>());
            }
            else
                return FindObjectsOfType<TMP_Text>(!advancedOptions.ExcludeInactive).ToList();
        }

        private List<KS_LanguageImageTranslate> GetAllImageTranslates()
        {
            return FindObjectsOfType<KS_LanguageImageTranslate>(true).ToList();
        }


        // ����Ҫ���� CurrentLanguage �ĵط����ô˷��������뵱ǰ���Դ���
        public void SetCurrentLanguage(string currentLanguageCode)
        {
            // ʹ�� LINQ ��ѯ�� LanguageList �л�ȡƥ��� LanguageData
            LanguageData language = LanguageList.FirstOrDefault(lang => lang.languageName == currentLanguageCode);

            // ����ҵ���ƥ��� LanguageData���������� CurrentLanguage
            if (language != null)
            {
                CurrentLanguage = language;
            }
            else
            {
                // ����Ҳ���ƥ��� LanguageData����ʹ��Ĭ������
                CurrentLanguage = LanguageList.FirstOrDefault(lang => lang.languageName == DefaultLanguage);
            }
        }

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(LangKey))
            {
                PlayerPrefs.SetString(LangKey, GetSystemDefaultLanguage());
            }
            InitiateLanguageDictionary();
        }

        // Start is called before the first frame update
        void Start()
        {
            //InitiateLanguageDictionary();
            SetLanguage();
        }
        
        /// <summary>
        /// ?????dict
        /// </summary>
        void InitiateLanguageDictionary()
        {
            string language;
            bool setPrefs = false;
            if (!ForcedSetLang)
            {
                language = PlayerPrefs.GetString(LangKey, DefaultLanguage);
                CurrentLang = language;
            }
            else
            {
                language = CurrentLang;
                setPrefs = true;
            }
            SetCurrentLanguage(language);
            LoadLanguageDictionary(language,setPrefs);
        }
        
        public void LoadLanguageDictionary(string language,bool set=false)
        {
            LanguageDictionary.Clear();
            CurrentLang = language;
            if (set) {
                PlayerPrefs.SetString(LangKey, language);
            }
            // string Path = "./Assets/Scripts/Language/" + CurrentLanguage.dictionaryPath;
            //string Path =  Application.streamingAssetsPath + '\\' + CurrentLanguage.dictionaryPath;
            // combine path
            // string path = Path.Combine(Application.streamingAssetsPath, CurrentLanguage.dictionaryPath);

            //if (language == "en")
            //{
            //    Path = "./Assets/Scripts/Language/english.txt";
            //}
            //else if (language == "ch" || language == "zh")
            //{
            //    Path = "./Assets/Scripts/Language/chinese.txt";
            //}
            //else if (language == "jp")
            //{
            //    Path = "./Assets/Scripts/Language/japanese.txt";
            //}
            //else
            //{
            //    Path = "./Assets/Scripts/Language/english.txt";
            //}


            //string[] raw_strs = File.ReadAllLines(@path, System.Text.Encoding.Default);

            LanguageDictionary = GetLangDictionary(CurrentLanguage);
        }


        public Dictionary<string, string> GetLangDictionary(LanguageData langData)
        {
            Dictionary<string, string> langDict = new Dictionary<string, string>();
            string textRawString = KS_ResourceManager.LoadResourceTXT(langData.dictionaryPath);
            // replace all \r\n to \n
            textRawString = textRawString.Replace("\r\n", "\n");
            string[] raw_strs = textRawString.Split('\n');

            for (int i = 0; i < raw_strs.Length; i++)
            {
                try
                {
                    if (raw_strs[i] != "" && raw_strs[i] != " " && raw_strs[i] != "  ")
                    {
                        if (raw_strs[i].Substring(0, 1) != "#")
                        {
                            string[] splited_strs = raw_strs[i].Split('|');
                            langDict.Add(splited_strs[0], splited_strs[1].Replace("\\n","\n"));
                        }
                    }
                }
                    
                catch (Exception e)
                {
                    if (LogErrorInfo)
                    KS_Logger.Log(e.ToString());
                }

            }

            return langDict;
        }

        public LanguageData GetCurrentData()
        {
            return CurrentLanguage;
        }

        public void ChangeLanguage(string lang, bool saveToPrefs = true)
        {
            if (saveToPrefs)
            {
                PlayerPrefs.SetString(LangKey, lang);
            }

            SetCurrentLanguage(lang);
            LoadLanguageDictionary(lang, true);
            KS_NotificationCenter.DispatchEvent("ChangeLang", CurrentLang);

            // set all language in scene, so we don't rely on KS_LanguageInitializer
            SetLanguage();
        }

        /// <summary>
        ///  ????
        /// </summary>
        [ContextMenu("SetLanguage")]
        public void SetLanguage()
        {
            if (LanguageDictionary.Count <= 1)
            {
                InitiateLanguageDictionary();
                KS_Logger.Log("no dictionary presents!");
            }

            List<TMP_Text> UIText;

            if (useCachedReferences)
            {
                UIText = TextsInScene;
            }
            else
            {
                UIText = GetAllTranslateObjects();
            }

            for (int i = 0; i < UIText.Count; i++)
            {
                if (UIText[i] == null)
                {
                    continue;
                }

                if (LanguageDictionary.ContainsKey(UIText[i].name))
                {
                    UIText[i].text = LanguageDictionary[UIText[i].name];
                    UIText[i].font = CurrentLanguage.defaultFont;
                }

                else if (advancedOptions.ChangeAllFonts)
                {
                    UIText[i].font = CurrentLanguage.defaultFont;    
                }
                //foreach (string key in LanguageDictionary.Keys)
                //{
                //    if (UIText[i].name == key)
                //    {
                //        UIText[i].GetComponent<TextMeshProUGUI>().text = LanguageDictionary[key];
                //        //UIText[i].GetComponent<TextMeshProUGUI>().text = LanguageDictionary[key];
                //    }
                //}
            }

            // translate Images
            List<KS_LanguageImageTranslate> imageTranslates;
            if (useCachedReferences)
            {
                imageTranslates = ImageTranslates;
            }
            else
            {
                imageTranslates = GetAllImageTranslates();
            }

            foreach (KS_LanguageImageTranslate imageTranslate in imageTranslates)
            {
                imageTranslate.TranslateImage();
            }

            KS_NotificationCenter.DispatchEvent(KS_InternalNotificationKeys.OnLanguageChanged, CurrentLang);
        }

        /// <summary>
        /// ????
        /// </summary>
        /// <param name="input_string"></param>
        /// <returns></returns>
        public string GetText(string input_string)
        {
            if (LanguageDictionary.Count <= 1)
            {
                InitiateLanguageDictionary();
            }
            if (LanguageDictionary.ContainsKey(input_string))
            {
                return LanguageDictionary[input_string];
            }

            // modified by Stanly: if the key is not found, return the key itself. This may be better?
            // return "";
            return input_string;
        }

        /// <summary>
        /// Useful for getting text out of editor play mode
        /// </summary>
        /// <param name="input_string"></param>
        /// <param name="lang">ch, en, jp.</param>
        /// <returns></returns>
        public string GetTextWithoutInit(string input_string, string lang)
        {
            LanguageData langData;
            switch (lang)
            {
                case "en":
                    langData = LanguageList.FirstOrDefault(x => x.languageName == "en");
                    break;
                case "ch":
                    langData = LanguageList.FirstOrDefault(x => x.languageName == "ch");
                    break;
                case "jp":
                    langData = LanguageList.FirstOrDefault(x => x.languageName == "jp");
                    break;
                default:
                    langData = LanguageList.FirstOrDefault(x => x.languageName == "en");
                    break;
            }
            Dictionary<string, string> langDict = GetLangDictionary(langData);
            if (langDict.ContainsKey(input_string))
            {
                return langDict[input_string];
            }
            return input_string;
        }

        public static void GetSystemLanguage()
        {
            KS_Logger.DebugLog(Application.systemLanguage);
        }

        [ContextMenu("GetSystemLanguage")]
        public void _GetSystemLanguage()
        {
            KS_Logger.DebugLog(Application.systemLanguage);
        }

        public static string GetSystemDefaultLanguage()
        {
            if(Application.systemLanguage == SystemLanguage.English)
            {
                return "en";
            }
            else if(Application.systemLanguage==SystemLanguage.Chinese)
            {
                return "ch";
            }
            else if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
            {
                return "ch";
            }
            else if (Application.systemLanguage == SystemLanguage.Japanese)
            {
                return "jp";
            }
            else
            {
                return DefaultLanguage;
            }
        }


        [Button("Check Missing Key Fields")]
        /// <summary>
        /// Load both english and chinese dictionary and check if there are any missing key fields
        /// </summary>
        public void CheckMissingKeyFields()
        {
            Dictionary<string, string> enDict = GetLangDictionary(LanguageList.FirstOrDefault(x => x.languageName == "en"));
            Dictionary<string, string> chDict = GetLangDictionary(LanguageList.FirstOrDefault(x => x.languageName == "ch"));

            foreach (string key in enDict.Keys)
            {
                if (!chDict.ContainsKey(key))
                {
                    KS_Logger.Log("Missing key in CHINESE dictionary: " + key);
                }
            }

            foreach (string key in chDict.Keys)
            {
                if (!enDict.ContainsKey(key))
                {
                    KS_Logger.Log("Missing key in ENGLISH dictionary: " + key);
                }
            }
        }
    }
}