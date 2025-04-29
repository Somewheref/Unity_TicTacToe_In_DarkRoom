using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;

public class DesktopWallpaperChanger : MonoBehaviour
{
     // The function for setting the wallpaper
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

    // Overload for getting the wallpaper
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(uint uiAction, uint uiParam, StringBuilder pvParam, uint fWinIni);

    private const uint SPI_SETDESKWALLPAPER = 20;
    private const uint SPI_GETDESKWALLPAPER = 115; 
    private const uint SPIF_UPDATEINIFILE   = 0x1;
    private const uint SPIF_SENDWININICHANGE = 0x2;

    /// <summary>
    /// Retrieves the path of the current wallpaper.
    /// </summary>
    /// <returns>Absolute path to the current wallpaper, or empty string if something fails</returns>
    public static string GetCurrentWallpaper()
    {
        StringBuilder sb = new StringBuilder(200); // 200 chars is usually enough for a file path
        int result = SystemParametersInfo(SPI_GETDESKWALLPAPER, (uint)sb.Capacity, sb, 0);
        if (result == 0)
        {
            // Something failed
            return string.Empty;
        }

        return sb.ToString();
    }

    // Call this method to change the desktop background
    public static bool SetWallpaper(string absolutePath)
    {
        // SPI_SETDESKWALLPAPER = 20
        // 0 = no extra parameter needed for wallpaper
        // absolutePath = path to the image file
        // SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE -> updates the user profile and notifies other apps
        int result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, absolutePath, 
                        SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        return result != 0; 
    }
}
