﻿using System;
using UnityEngine;

namespace Crosstales.Common.Util
{
   /// <summary>Base for collected constants of very general utility for the asset.</summary>
   public abstract class BaseConstants
   {
      #region Constant variables

      /// <summary>Version of the Common.</summary>
      public const string COMMON_VERSION = "2024.1.1";

      /// <summary>Build number of the Common.</summary>
      public const int COMMON_BUILD = 20240226;

      /// <summary>Change date of the Common (YYYY, MM, DD).</summary>
      public static readonly DateTime COMMON_CHANGED = new DateTime(2024, 3, 15);

      /// <summary>Author of the asset.</summary>
      public const string ASSET_AUTHOR = "crosstales LLC";

      /// <summary>URL of the asset author.</summary>
      public const string ASSET_AUTHOR_URL = "https://www.crosstales.com";

      /// <summary>URL of the crosstales assets in UAS.</summary>
      public const string ASSET_CT_URL = "https://assetstore.unity.com/lists/crosstales-42213?aid=1011lNGT";

      /// <summary>URL of the crosstales Discord-channel.</summary>
      public const string ASSET_SOCIAL_DISCORD = "https://discord.gg/ZbZ2sh4";

      /// <summary>URL of the crosstales Facebook-profile.</summary>
      public const string ASSET_SOCIAL_FACEBOOK = "https://www.facebook.com/crosstales/";

      /// <summary>URL of the crosstales Twitter-profile.</summary>
      public const string ASSET_SOCIAL_TWITTER = "https://twitter.com/crosstales";

      /// <summary>URL of the crosstales Youtube-profile.</summary>
      public const string ASSET_SOCIAL_YOUTUBE = "https://www.youtube.com/c/Crosstales";

      /// <summary>URL of the crosstales LinkedIn-profile.</summary>
      public const string ASSET_SOCIAL_LINKEDIN = "https://www.linkedin.com/company/crosstales";

      /// <summary>URL of the 3rd party asset "PlayMaker".</summary>
      public const string ASSET_3P_PLAYMAKER = "https://assetstore.unity.com/packages/slug/368?aid=1011lNGT";

      /// <summary>URL of the 3rd party asset "Volumetric Audio".</summary>
      public const string ASSET_3P_VOLUMETRIC_AUDIO = "https://assetstore.unity.com/packages/slug/17125?aid=1011lNGT";

      /// <summary>URL of the 3rd party asset "RockTomate".</summary>
      public const string ASSET_3P_ROCKTOMATE = "https://assetstore.unity.com/packages/slug/156311?aid=1011lNGT";

      /// <summary>URL of the 3rd party asset "Runtime File Browser".</summary>
      public const string ASSET_3P_RTFB = "https://assetstore.unity.com/packages/slug/113006?aid=1011lNGT";

      /// <summary>URL of the "Badword Filter" asset.</summary>
      public const string ASSET_BWF = "https://assetstore.unity.com/packages/slug/26255?aid=1011lNGT";

      /// <summary>URL of the "DJ" asset.</summary>
      public const string ASSET_DJ = "https://assetstore.unity.com/packages/slug/41993?aid=1011lNGT";

      /// <summary>URL of the "File Browser" asset.</summary>
      public const string ASSET_FB = "https://assetstore.unity.com/packages/slug/98713?aid=1011lNGT";

      /// <summary>URL of the "Online Check" asset.</summary>
      public const string ASSET_OC = "https://assetstore.unity.com/packages/slug/74688?aid=1011lNGT";

      /// <summary>URL of the "Radio" asset.</summary>
      public const string ASSET_RADIO = "https://assetstore.unity.com/packages/slug/32034?aid=1011lNGT";

      /// <summary>URL of the "RT-Voice" asset.</summary>
      public const string ASSET_RTV = "https://assetstore.unity.com/packages/slug/41068?aid=1011lNGT";

      /// <summary>URL of the "Turbo Backup" asset.</summary>
      public const string ASSET_TB = "https://assetstore.unity.com/packages/slug/98711?aid=1011lNGT";

      /// <summary>URL of the "Turbo Builder" asset.</summary>
      public const string ASSET_TPB = "https://assetstore.unity.com/packages/slug/98714?aid=1011lNGT";

      /// <summary>URL of the "Turbo Switch" asset.</summary>
      public const string ASSET_TPS = "https://assetstore.unity.com/packages/slug/60040?aid=1011lNGT";

      /// <summary>URL of the "True Random" asset.</summary>
      public const string ASSET_TR = "https://assetstore.unity.com/packages/slug/61617?aid=1011lNGT";

      /// <summary>Factor for kilo bytes.</summary>
      public const int FACTOR_KB = 1024;

      /// <summary>Factor for mega bytes.</summary>
      public const int FACTOR_MB = FACTOR_KB * 1024;

      /// <summary>Factor for giga bytes.</summary>
      public const int FACTOR_GB = FACTOR_MB * 1024;

      /// <summary>Float value of 32768.</summary>
      public const float FLOAT_32768 = 32768f;

      /// <summary>Float tolerance.</summary>
      public const float FLOAT_TOLERANCE = 0.0001f;

      /// <summary>ToString for two decimal places.</summary>
      public const string FORMAT_TWO_DECIMAL_PLACES = "0.00";

      /// <summary>ToString for no decimal places.</summary>
      public const string FORMAT_NO_DECIMAL_PLACES = "0";

      /// <summary>ToString for percent.</summary>
      public const string FORMAT_PERCENT = "0%";


      // Default values
      public const bool DEFAULT_DEBUG = false;

      /// <summary>Path delimiter for Windows.</summary>
      public const string PATH_DELIMITER_WINDOWS = @"\";

      /// <summary>Path delimiter for Unix.</summary>
      public const string PATH_DELIMITER_UNIX = "/";


      private static System.Text.RegularExpressions.Regex _regexLineEndings;
      public static System.Text.RegularExpressions.Regex REGEX_LINEENDINGS => _regexLineEndings ?? (_regexLineEndings = new System.Text.RegularExpressions.Regex(@"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+"));

      private static System.Text.RegularExpressions.Regex _regexEmail;
      public static System.Text.RegularExpressions.Regex REGEX_EMAIL => _regexEmail ?? (_regexEmail = new System.Text.RegularExpressions.Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"));

      private static System.Text.RegularExpressions.Regex _regexCreditCard;
      public static System.Text.RegularExpressions.Regex REGEX_CREDITCARD => _regexCreditCard ?? (_regexCreditCard = new System.Text.RegularExpressions.Regex(@"^((\d{4}[- ]?){3}\d{4})$"));

      private static System.Text.RegularExpressions.Regex _regexUrlWeb;
      public static System.Text.RegularExpressions.Regex REGEX_URL_WEB => _regexUrlWeb ?? (_regexUrlWeb = new System.Text.RegularExpressions.Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$"));

      private static System.Text.RegularExpressions.Regex _regexIPAddress;
      public static System.Text.RegularExpressions.Regex REGEX_IP_ADDRESS => _regexIPAddress ?? (_regexIPAddress = new System.Text.RegularExpressions.Regex(@"^([0-9]{1,3}\.){3}[0-9]{1,3}$"));

      private static System.Text.RegularExpressions.Regex _regexInvalidChars;
      public static System.Text.RegularExpressions.Regex REGEX_INVALID_CHARS => _regexInvalidChars ?? (_regexInvalidChars = new System.Text.RegularExpressions.Regex(@"[^\w\.@-]"));

      private static System.Text.RegularExpressions.Regex _regexAlpha;
      public static System.Text.RegularExpressions.Regex REGEX_ALPHANUMERIC => _regexAlpha ?? (_regexAlpha = new System.Text.RegularExpressions.Regex(@"([A-Za-z0-9_]+)"));

      private static System.Text.RegularExpressions.Regex _regexCleanSpace;
      public static System.Text.RegularExpressions.Regex REGEX_CLEAN_SPACES => _regexCleanSpace ?? (_regexCleanSpace = new System.Text.RegularExpressions.Regex(@"\s+"));

      private static System.Text.RegularExpressions.Regex _regexCleanTags;
      public static System.Text.RegularExpressions.Regex REGEX_CLEAN_TAGS => _regexCleanTags ?? (_regexCleanTags = new System.Text.RegularExpressions.Regex(@"<.*?>"));

      private static System.Text.RegularExpressions.Regex _regexDriveLetters;
      public static System.Text.RegularExpressions.Regex REGEX_DRIVE_LETTERS => _regexDriveLetters ?? (_regexDriveLetters = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]:"));

      private static System.Text.RegularExpressions.Regex _regexFile;

      //public static System.Text.RegularExpressions.Regex REGEX_FILE => _regexFile ?? (_regexFile = new System.Text.RegularExpressions.Regex(@"^\.[\w]+$"));
      public static System.Text.RegularExpressions.Regex REGEX_FILE => _regexFile ?? (_regexFile = new System.Text.RegularExpressions.Regex(@"^.*\.[\w]+$"));

      //public static readonly System.Text.RegularExpressions.Regex asciiOnlyRegex = new System.Text.RegularExpressions.Regex(@"[^\u0000-\u00FF]+");
      //public static readonly System.Text.RegularExpressions.Regex REGEX_REALNUMBER = new System.Text.RegularExpressions.Regex(@"([-+]?[0-9]*\.?[0-9]+)");
      //public static readonly System.Text.RegularExpressions.Regex REGEX_SIGNED_INTEGER = new System.Text.RegularExpressions.Regex(@"([-+]?[0-9]+)");
      //public static readonly Regex cleanStringRegex = new Regex(@"([^a-zA-Z0-9 ]|[ ]{2,})");

      public const string ALPHABET_LATIN_UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      public const string ALPHABET_LATIN_LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
      public const string ALPHABET_EXT_UPPERCASE = "ÀÂÄÆÇÈÉÊËÎÏÔŒÙÛÜ";
      public const string ALPHABET_EXT_LOWERCASE = "àâäæçèéêëîïôœùûü";
      public static readonly string ALPHABET_LATIN = $"{ALPHABET_LATIN_UPPERCASE}{ALPHABET_EXT_UPPERCASE}{ALPHABET_LATIN_LOWERCASE}{ALPHABET_EXT_LOWERCASE}";

      public const string NUMBERS = "0123456789";

      public static readonly string SIGNS = $"{ALPHABET_LATIN}{NUMBERS}";

      #endregion


      #region Changable variables

      /// <summary>Development debug logging for the asset.</summary>
      public static bool DEV_DEBUG = false;

      // Text fragments for the asset
      public static string TEXT_TOSTRING_START = " {";
      public static string TEXT_TOSTRING_END = "}";
      public static string TEXT_TOSTRING_DELIMITER = "', ";
      public static string TEXT_TOSTRING_DELIMITER_END = "'";

      // Prefixes for URLs and paths
      public const string PREFIX_HTTP = "http://";

      public const string PREFIX_HTTPS = "https://";
      //public const string PREFIX_SMB = "smb://";
      //public const string PREFIX_FTP = "ftp://";

      /// <summary>Kill processes after 5000 milliseconds.</summary>
      public static int PROCESS_KILL_TIME = 5000;

      /// <summary>Path to the cmd under Windows.</summary>
      public static string CMD_WINDOWS_PATH = @"C:\Windows\system32\cmd.exe";

      /// <summary>Show the BWF banner.</summary>
      public static bool SHOW_BWF_BANNER = true;

      /// <summary>Show the DJ banner.</summary>
      public static bool SHOW_DJ_BANNER = true;

      /// <summary>Show the FB banner.</summary>
      public static bool SHOW_FB_BANNER = true;

      /// <summary>Show the OC banner.</summary>
      public static bool SHOW_OC_BANNER = true;

      /// <summary>Show the Radio banner.</summary>
      public static bool SHOW_RADIO_BANNER = true;

      /// <summary>Show the RTV banner.</summary>
      public static bool SHOW_RTV_BANNER = true;

      /// <summary>Show the TB banner.</summary>
      public static bool SHOW_TB_BANNER = true;

      /// <summary>Show the TPB banner.</summary>
      public static bool SHOW_TPB_BANNER = true;

      /// <summary>Show the TPS banner.</summary>
      public static bool SHOW_TPS_BANNER = true;

      /// <summary>Show the TR banner.</summary>
      public static bool SHOW_TR_BANNER = true;

      #endregion


      #region Properties

      /// <summary>URL prefix for files.</summary>
      public static string PREFIX_FILE
      {
         get
         {
            if ((Crosstales.Common.Util.BaseHelper.isWindowsBasedPlatform || Crosstales.Common.Util.BaseHelper.isWindowsEditor) && !Crosstales.Common.Util.BaseHelper.isMacOSEditor && !Crosstales.Common.Util.BaseHelper.isLinuxEditor)
            {
               return "file:///";
            }

            return "file://";
         }
      }

      /// <summary>Application path.</summary>
      public static string APPLICATION_PATH => Crosstales.Common.Util.FileHelper.ValidatePath(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/') + 1));

      #endregion
   }
}
// © 2015-2024 crosstales LLC (https://www.crosstales.com)