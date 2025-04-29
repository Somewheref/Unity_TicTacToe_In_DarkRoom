using System;
using System.Collections;
using System.Collections.Generic;
using KayphoonStudio;
using UnityEngine;

using UnityEngine.Events;

public class KS_NotificationEventInvoker : MonoBehaviour
{
    public bool logWhenCalled = false;

    [System.Serializable]
    public class NotificationEvent
    {
        public string key;
        public UnityEvent onEvent;
    }

    public List<NotificationEvent> notificationEvents;

    private void Awake()
    {
        foreach (var item in notificationEvents)
        {
            KS_NotificationCenter.AddEventListener(item.key, (Notification _) => {
                OnNotificationEvent(item);
            });
        }
    }

    void OnNotificationEvent(NotificationEvent eventItem)
    {
        if (logWhenCalled)
            KS_Logger.Log("Invoking Event: " + eventItem.key, this); 
        eventItem.onEvent?.Invoke(); 
    }

    // private void OnDestroy() {
    //     foreach (NotificationEvent eventItem in notificationEvents)
    //     {
    //         KS_NotificationCenter.RemoveSingleEventListener(eventItem.key, OnNotificationEvent);
    //     }
    // }
}
