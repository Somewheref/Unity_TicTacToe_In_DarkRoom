using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio
{
    /// <summary>
    /// Notification delegate
    /// </summary>
    public delegate void OnNotification(Notification notific);

    /// <summary>
    /// Notification class
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Notification sender
        /// </summary>
        public GameObject sender;

        /// <summary>
        /// Notification parameter
        /// </summary>
        public object param;

        /// <summary>
        /// Notification constructor
        /// </summary>
        /// <param name="sender">Notification sender</param>
        /// <param name="param">Notification parameter</param>
        public Notification(GameObject sender, object param)
        {
            this.sender = sender;
            this.param = param;
        }

        /// <summary>
        /// Notification constructor with only parameter
        /// </summary>
        /// <param name="param">Notification parameter</param>
        public Notification(object param)
        {
            this.sender = null;
            this.param = param;
        }

        /// <summary>
        /// Override ToString() to print notification information
        /// </summary>
        /// <returns>Notification information</returns>
        public override string ToString()
        {
            return string.Format("sender={0},param={1}", this.sender, this.param);
        }
    }

    /// <summary>
    /// Notification center
    /// </summary>
    public class KS_NotificationCenter:KS_Singleton<KS_NotificationCenter>
    {
        public bool LogInfo = false;
        /// <summary>
        /// Event listeners
        /// </summary>
        private Dictionary<string, OnNotification> eventListeners
        = new Dictionary<string, OnNotification>();

        /// <summary>
        /// Add event listener
        /// </summary>
        /// <param name="eventKey">Event key</param>
        /// <param name="eventListener">Event listener</param>
        public void addEventListener(string eventKey, OnNotification eventListener)
        {
            if (!eventListeners.ContainsKey(eventKey))
            {
                eventListeners.Add(eventKey, eventListener);
            }
            else
            {
                eventListeners[eventKey] += eventListener;
            }
        }

        public void removeEventListener(string eventKey)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;

            eventListeners[eventKey] = null;
            eventListeners.Remove(eventKey);
        }

        public void removeSingleEventListener(string eventKey, OnNotification eventListener)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;

            eventListeners[eventKey] -= eventListener;
        }

        /// <summary>
        /// Dispatch notification
        /// </summary>
        /// <param name="eventKey">Event key</param>
        /// <param name="notific">Notification</param>
        public void dispatchEvent(string eventKey, Notification notific)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            var content = notific;
            try
            {
                eventListeners[eventKey](content);
            }
            catch
            {
                try
                {
                    Delegate[] delegates = eventListeners[eventKey].GetInvocationList();

                    foreach (OnNotification singleDelegate in delegates)
                    {
                        try
                        {
                            singleDelegate(content);
                        }
                        catch
                        {
                            eventListeners[eventKey] -= singleDelegate;
                            // Logging.Log("Removed one delegate!");
                        }

                    }
                }
                
                catch
                {
                    if (LogInfo)
                    {
                        KS_Logger.LogWarning("Does not have any delegate for key '" + eventKey + "'", this);
                    }
                }
            }
        }

        /// <summary>
        /// Dispatch notification with sender and parameter
        /// </summary>
        /// <param name="eventKey">Event key</param>
        /// <param name="sender">Notification sender</param>
        /// <param name="param">Notification parameter</param>
        public void dispatchEvent(string eventKey, GameObject sender, object param)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            var content = new Notification(sender, param);

            if (eventListeners[eventKey] == null)
            {
                return;
            }

            Delegate[] delegates = eventListeners[eventKey].GetInvocationList();

            foreach (OnNotification singleDelegate in delegates)
            {
                try
                {
                    singleDelegate(content);
                }
                catch(Exception e)
                {
                    eventListeners[eventKey] -= singleDelegate;
                    if (LogInfo)
                    {
                        KS_Logger.LogWarning("Removed one delegate from key '" + eventKey + "! Delegate Info: " +  singleDelegate.Method.Name + " Error message: " + e.Message, this);
                    }
                }
                
            }
        }

        /// <summary>
        /// Dispatch notification with only parameter
        /// </summary>
        /// <param name="eventKey">Event key</param>
        /// <param name="param">Notification parameter</param>
        public void dispatchEvent(string eventKey, object param)
        {
            if (!eventListeners.ContainsKey(eventKey))
                return;
            var content = new Notification(param);

            if (eventListeners[eventKey] == null)
            {
                return;
            }

            Delegate[] delegates = eventListeners[eventKey].GetInvocationList();

            foreach (OnNotification singleDelegate in delegates)
            {
                try
                {
                    singleDelegate(content);
                }
                catch (Exception e)
                {
                    eventListeners[eventKey] -= singleDelegate;
                    if (LogInfo)
                    {
                        KS_Logger.LogWarning("Removed one delegate from key '" + eventKey + "! Delegate Info: " +  singleDelegate.Method.Name + " Error message: " + e.Message, this);
                    }
                }

            }
        }

        /// <summary>
        /// Check if there is an event listener for the event key
        /// </summary>
        /// <param name="eventKey">Event key</param>
        /// <returns>True if there is an event listener for the event key, false otherwise</returns>
        public bool hasEventListener(string eventKey)
        {
            return eventListeners.ContainsKey(eventKey);
        }

        #region Static Methods

        public static void AddEventListener(string eventKey, OnNotification eventListener)
        {
            Instance.addEventListener(eventKey, eventListener);
        }

        public static void AddEventListener(string eventKey, Action onEvent)
        {
            Instance.addEventListener(eventKey, (notific) => onEvent());
        }

        public static void RemoveEventListener(string eventKey)
        {
            Instance.removeEventListener(eventKey);
        }

        public static void DispatchEvent(string eventKey)
        {
            Instance.dispatchEvent(eventKey, null);
        }

        public static void DispatchEvent(string eventKey, Notification notific)
        {
            Instance.dispatchEvent(eventKey, notific);
        }

        public static void DispatchEvent(string eventKey, GameObject sender, object param)
        {
            Instance.dispatchEvent(eventKey, sender, param);
        }

        public static void DispatchEvent(string eventKey, object param)
        {
            Instance.dispatchEvent(eventKey, param);
        }

        public static void RemoveSingleEventListener(string eventKey, OnNotification param)
        {
            if (!Instance) return;
            Instance.removeSingleEventListener(eventKey, param);
        }


        #endregion

    }
}