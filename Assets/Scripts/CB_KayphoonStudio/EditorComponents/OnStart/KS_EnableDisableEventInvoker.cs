using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KayphoonStudio.EditorUtilities
{
    public class KS_EnableDisableEventInvoker : KayphoonStudio.BaseClass.KS_InvokerBase
    {
        public UnityEvent OnEnableEvent;
        public UnityEvent OnDisableEvent;

        [ContextMenu("Execute OnEnable Event")]
        public void ExecuteOnEnableEvent()
        {
            OnEnableEvent?.Invoke();
        }

        [ContextMenu("Execute OnDisable Event")]
        public void ExecuteOnDisableEvent()
        {
            OnDisableEvent?.Invoke();
        }

        private void OnEnable()
        {
            ExecuteOnEnableEvent();
        }

        private void OnDisable()
        {
            ExecuteOnDisableEvent();
        }
    }

}
