using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_UI_MarkableProgressBar_Marker : MonoBehaviour
{
    public KS_UI_MarkableProgressBar_Controller controller;
    public float value;

    protected KS_UI_MarkableProgressBar_Marker self;

    protected virtual void Reset() 
    {
        controller = GetComponentInParent<KS_UI_MarkableProgressBar_Controller>();
    }

    private void Awake() {
        self = this;
    }

    public virtual void OnEnable() 
    {
        if (self == null) self = this;
        controller.SetPosition(value, ref self, OnPassing);
    }

    public virtual void SetValue(float val)
    {
        value = val;
        
        controller.SetPosition(value, ref self, OnPassing);
    }

    public virtual void OnPassing()
    {

    }
}
