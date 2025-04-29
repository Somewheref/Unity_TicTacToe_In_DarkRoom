#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class UserClipboardUtils
{
    // Clipboard formats
    private const uint CF_UNICODETEXT = 13;

    // Global memory flags
    private const uint GMEM_MOVEABLE = 0x0002;

    // --- Win32 Functions ---

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalUnlock(IntPtr hMem);

    // --- Public Methods ---

    /// <summary>
    /// Copies the given string into the user's clipboard as Unicode text.
    /// Returns true if successful, false otherwise.
    /// </summary>
    public static bool CopyToClipboard(string text)
    {
        if (string.IsNullOrEmpty(text)) text = "";

        // Open the clipboard
        if (!OpenClipboard(IntPtr.Zero))
        {
            Debug.LogWarning("Could not open clipboard.");
            return false;
        }

        try
        {
            // Clear existing clipboard data
            if (!EmptyClipboard())
            {
                Debug.LogWarning("Could not empty clipboard.");
            }

            // Calculate memory size in bytes for the string + null terminator
            // Each char is 2 bytes in UTF-16
            int bytes = (text.Length + 1) * 2;

            // Allocate a movable global memory object for the text
            IntPtr hGlobal = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)bytes);
            if (hGlobal == IntPtr.Zero)
            {
                Debug.LogWarning("Failed to allocate memory for clipboard data.");
                return false;
            }

            // Lock the block to get a pointer
            IntPtr target = GlobalLock(hGlobal);
            if (target == IntPtr.Zero)
            {
                Debug.LogWarning("Failed to lock clipboard memory.");
                return false;
            }

            try
            {
                // Copy the string (as UTF-16) to the memory
                // We can use Marshal.Copy with a char[] or directly write bytes
                char[] chars = text.ToCharArray();
                Marshal.Copy(chars, 0, target, chars.Length);

                // Write the null terminator (2 bytes) at the end
                Marshal.WriteInt16(target, text.Length * 2, 0);
            }
            finally
            {
                // Unlock
                GlobalUnlock(hGlobal);
            }

            // Place the handle on the clipboard
            IntPtr result = SetClipboardData(CF_UNICODETEXT, hGlobal);
            if (result == IntPtr.Zero)
            {
                Debug.LogWarning("SetClipboardData failed.");
                return false;
            }
        }
        finally
        {
            // Close the clipboard
            CloseClipboard();
        }

        Debug.Log($"Copied to clipboard: \"{text}\"");
        return true;
    }

    /// <summary>
    /// Reads and returns the current text (Unicode) from the clipboard.
    /// Returns an empty string if there's no valid text or an error occurs.
    /// </summary>
    public static string ReadFromClipboard()
    {
        // Open the clipboard
        if (!OpenClipboard(IntPtr.Zero))
        {
            Debug.LogWarning("Could not open clipboard to read.");
            return string.Empty;
        }

        string clipboardText = string.Empty;

        try
        {
            // Get handle to the clipboard data
            IntPtr handle = GetClipboardData(CF_UNICODETEXT);
            if (handle == IntPtr.Zero)
            {
                Debug.Log("No Unicode text in clipboard.");
                return string.Empty;
            }

            // Lock the global memory to get a pointer to the text
            IntPtr pointer = GlobalLock(handle);
            if (pointer == IntPtr.Zero)
            {
                Debug.LogWarning("Failed to lock clipboard data.");
                return string.Empty;
            }

            try
            {
                // Determine the length by scanning for a null terminator
                int length = 0;
                while (Marshal.ReadInt16(pointer, length * 2) != 0)
                {
                    length++;
                }

                // Read the characters
                char[] buffer = new char[length];
                Marshal.Copy(pointer, buffer, 0, length);

                // Convert to string
                clipboardText = new string(buffer);
            }
            finally
            {
                GlobalUnlock(handle);
            }
        }
        finally
        {
            // Close the clipboard
            CloseClipboard();
        }

        Debug.Log($"Clipboard read: \"{clipboardText}\"");
        return clipboardText;
    }
}
#else
// If not Windows, we just provide empty stubs to avoid compilation errors
public static class UserClipboardUtils
{
    public static bool CopyToClipboard(string text)
    {
        UnityEngine.Debug.LogWarning("Clipboard unsupported on this platform.");
        return false;
    }

    public static string GetFromClipboard()
    {
        UnityEngine.Debug.LogWarning("Clipboard unsupported on this platform.");
        return string.Empty;
    }
}
#endif
