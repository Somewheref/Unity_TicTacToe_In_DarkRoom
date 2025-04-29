using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public static class KS_DataSaveWriter
    {
        public static string WriteByteToDevice(byte[] data, string fileName, KS_DataTypes.FilePathType pathType = KS_DataTypes.FilePathType.PersistentDataPath, bool autoAddIndex = true)
        {
            string filePath = GetAvailableFilePath(pathType, fileName, autoAddIndex);
            System.IO.File.WriteAllBytes(filePath, data);
            KS_Logger.Log("KS_DataSaveWriter: Saved file to " + filePath);
            return filePath;
        }


        public static string GetAvailableFilePath(KS_DataTypes.FilePathType filePathType, string fileName, bool autoAddIndex = true)
        {
            string path = GetPath(filePathType);
            if (autoAddIndex)
            {
                int index = 0;
                string newPath = path + "/" + fileName;
                while (System.IO.File.Exists(newPath))
                {
                    index++;
                    newPath = AddIndexToFileName(path + "/" + fileName, index);
                }
                return newPath;
            }
            else
            {
                return path + "/" + fileName;
            }
        }

        public static string AddIndexToFileName(string fileName, int index)
        {
            // the filename has an extension, add the index before the extension
            if (fileName.Contains("."))
            {
                string[] parts = fileName.Split('.');
                // combine the parts except the last one
                string newFileName = "";
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    newFileName += parts[i];
                }
                return newFileName + index + "." + parts[parts.Length - 1];
            }
            else
            {
                return fileName + index;
            }
        }
        

        public static string GetPath(KS_DataTypes.FilePathType pathType)
        {
            switch (pathType)
            {
                case KS_DataTypes.FilePathType.PersistentDataPath:
                    return Application.persistentDataPath;
                case KS_DataTypes.FilePathType.StreamingAssetsPath:
                    return Application.streamingAssetsPath;
                case KS_DataTypes.FilePathType.TemporaryCachePath:
                    return Application.temporaryCachePath;
                case KS_DataTypes.FilePathType.DataPath:
                    return Application.dataPath;
                case KS_DataTypes.FilePathType.CustomPath:
                    return "";
                default:
                    return "";
            }
        }
    }
}
