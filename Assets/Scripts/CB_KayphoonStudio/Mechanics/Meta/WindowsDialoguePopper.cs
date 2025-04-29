

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KayphoonStudio;

#if UNITY_STANDALONE_WIN
using System.Runtime.InteropServices;
#endif

public class WindowsDialoguePopper : MonoBehaviour
{
    #if UNITY_STANDALONE_WIN
    [DllImport("user32.dll")]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    
    private const uint MB_OK = 0x0;
    private const uint MB_OKCANCEL = 0x1;
    private const uint MB_ICONINFORMATION = 0x40;
    private const uint MB_ICONWARNING = 0x30;
    #endif

    public enum DialogButtons
    {
        OK,
        OKCancel
    }

    public enum DialogIcon
    {
        Information,
        Warning
    }

    public static DialogResult ShowSystemDialog(string title, string message, DialogButtons buttons, DialogIcon icon)
    {
        #if UNITY_STANDALONE_WIN
        uint messageBoxType = MB_OK;
        if (buttons == DialogButtons.OK)
            messageBoxType = MB_OK;
        else if (buttons == DialogButtons.OKCancel)
            messageBoxType = MB_OKCANCEL;

        if (icon == DialogIcon.Information)
            messageBoxType |= MB_ICONINFORMATION;
        else
            messageBoxType |= MB_ICONWARNING;

        int result = MessageBox(IntPtr.Zero, message, title, messageBoxType);

        if (result == 1)  // IDOK
        {
            KS_Logger.Log("User clicked OK!");
            return DialogResult.OK;
        }
        else
        {
            KS_Logger.Log("User clicked Cancel or closed the box.");
            return DialogResult.Cancel;
        }
        #else
        KS_Logger.Log("System dialog not supported on this platform.");
        #endif
        return DialogResult.Abort;
    }


    public enum DialogResult
    {
        OK,
        Cancel,
        Abort
    }
}
