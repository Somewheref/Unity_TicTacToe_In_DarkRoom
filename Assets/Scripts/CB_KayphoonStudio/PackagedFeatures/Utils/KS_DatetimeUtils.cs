using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public class KS_DatetimeUtils : MonoBehaviour
    {
        public static string GetUTCTimeStringFromTimestamp(float timestamp)
        {
            // ��Unixʱ���ת��ΪDateTime����
            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).DateTime;
            // ��DateTime�����ʽ��Ϊ������ַ�����ʽ
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTimeStringFromTimestamp(float timestamp)
        {
            // ��Unixʱ���ת��ΪDateTimeOffset����
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)timestamp);

            // ��ȡϵͳ��ǰʱ����Ϣ
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            // ��ʱ��ת��Ϊϵͳ��ǰʱ����ʱ��
            DateTime dateTimeInLocalTimeZone = TimeZoneInfo.ConvertTime(dateTimeOffset, localTimeZone).DateTime;

            // ��DateTime�����ʽ��Ϊ������ַ�����ʽ
            return dateTimeInLocalTimeZone.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
