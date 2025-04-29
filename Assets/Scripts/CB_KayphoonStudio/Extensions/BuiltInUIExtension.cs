using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public static class KS_UI_BuiltInExtensions
{

    #region Slient Set Value
    // All the methods in this region changes the value of UI without firing the callback event.
    static Slider.SliderEvent emptySliderEvent = new Slider.SliderEvent();
    public static void KS_SilentSetValue(this Slider instance, float value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptySliderEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }
 
    static Toggle.ToggleEvent emptyToggleEvent = new Toggle.ToggleEvent();
    public static void KS_SilentSetValue(this Toggle instance, bool value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyToggleEvent;
        instance.isOn = value;
        instance.onValueChanged = originalEvent;
    }
 
    static InputField.OnChangeEvent emptyInputFieldEvent = new InputField.OnChangeEvent();
    public static void KS_SilentSetValue(this InputField instance, string value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyInputFieldEvent;
        instance.text = value;
        instance.onValueChanged = originalEvent;
    }
 
    static Dropdown.DropdownEvent emptyDropdownFieldEvent = new Dropdown.DropdownEvent();
    public static void KS_SilentSetValue(this Dropdown instance, int value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyDropdownFieldEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }

    // TMP dropdown
    static TMP_Dropdown.DropdownEvent emptyTMPDropdownFieldEvent = new TMP_Dropdown.DropdownEvent();
    public static void KS_SilentSetValue(this TMP_Dropdown instance, int value)
    {
        var originalEvent = instance.onValueChanged;
        instance.onValueChanged = emptyTMPDropdownFieldEvent;
        instance.value = value;
        instance.onValueChanged = originalEvent;
    }
 
    // Modify button normal color
    public static void KS_SetNormalColor(this Button instance, Color color)
    {
        var colors = instance.colors;
        colors.normalColor = color;
        instance.colors = colors;
    }
    
    #endregion
}
