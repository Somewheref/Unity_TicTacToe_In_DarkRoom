using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;
using KayphoonStudio.BaseClass;

namespace KayphoonStudio.EditorUtilities
{
    public class KS_EventInvoker : KS_InvokerBase
    {
        public bool InvokeOnStart = false;
        public UnityEvent OnExecute;


        protected virtual void Start()
        {
            if (InvokeOnStart)
            {
                InvokeEvent();
            }
        }

        public virtual void InvokeEvent()
        {
            StartCoroutine(KS_Delayer.Delay(_actualStartDelay, () => { ExecuteEvent(); }));
        }

        public virtual void ExecuteEvent()
        {
            OnExecute?.Invoke();
        }

        
    }
}
