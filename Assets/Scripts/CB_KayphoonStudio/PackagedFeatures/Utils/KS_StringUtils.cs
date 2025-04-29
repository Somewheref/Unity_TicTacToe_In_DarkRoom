using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_StringUtils : MonoBehaviour
    {
        public static string GenerateRandomString(int length)
        {
            StringBuilder builder = new StringBuilder();
            System.Random rand = new System.Random();

            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(System.Math.Floor(26 * rand.NextDouble() + 65))); //����A-Z���ַ�
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string DictionaryToString(Dictionary<string, object> dict)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, object> pair in dict)
            {
                builder.Append(pair.Key);
                builder.Append(": ");
                builder.Append(pair.Value);
                builder.Append(", ");
            }

            return builder.ToString();
        }
    }
}

