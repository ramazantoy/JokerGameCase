using System;
using System.Collections.Generic;

namespace _Project.Scripts.Events
{
    public  class EventBus 
    {
        private static EventBus _instance;
        private Dictionary<EventType, Delegate> _eventListeners = new Dictionary<EventType, Delegate>();

        private EventBus () { }

        public static EventBus  Instance
        {
            get { return _instance ??= new EventBus (); }
        }

        public void AddListener<T1,T2>(EventType eventType, Action<T1,T2> listener)
        {
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = null;
            }
            _eventListeners[eventType] = (Action<T1,T2>)_eventListeners[eventType] + listener;
        }
        
        public void AddListener<T>(EventType eventType, Action<T> listener)
        {
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = null;
            }
            _eventListeners[eventType] = (Action<T>)_eventListeners[eventType] + listener;
        }
        
        public void AddListener(EventType eventType, Action listener)
        {
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = null;
            }
            _eventListeners[eventType] = (Action)_eventListeners[eventType] + listener;
        }

        public void RemoveListener<T>(EventType eventType, Action<T> listener)
        {
            if (_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = (Action<T>)_eventListeners[eventType] - listener;
            }
        }

        public void TriggerEvent<T>(EventType eventType, T eventData)
        {
            if (_eventListeners.ContainsKey(eventType))
            {
                ((Action<T>)_eventListeners[eventType])?.Invoke(eventData);
            }
        }
    }
}
