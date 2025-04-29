using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KayphoonStudio.Language
{
    [CreateAssetMenu(fileName = "NewLanguage", menuName = "Language Data")]
    public class LanguageData : ScriptableObject
    {
        public string languageName;    // ��������
        public TMP_FontAsset defaultFont;       // Ĭ������
        public string dictionaryPath;  // �ֵ��ļ�·����
    }
}
