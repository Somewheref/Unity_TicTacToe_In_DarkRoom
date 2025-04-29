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
            // 将Unix时间戳转换为DateTime对象
            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).DateTime;
            // 将DateTime对象格式化为所需的字符串格式
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTimeStringFromTimestamp(float timestamp)
        {
            // 将Unix时间戳转换为DateTimeOffset对象
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)timestamp);

            // 获取系统当前时区信息
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            // 将时间转换为系统当前时区的时间
            DateTime dateTimeInLocalTimeZone = TimeZoneInfo.ConvertTime(dateTimeOffset, localTimeZone).DateTime;

            // 将DateTime对象格式化为所需的字符串格式
            return dateTimeInLocalTimeZone.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
