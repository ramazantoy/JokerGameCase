using System;
using System.Collections.Generic;

namespace _Project.Scripts.Events
{
    public  class EventBus 
    {
        private static EventBus _instance;
        private Dictionary<Type, List<Action<object>>> _eventListeners = new Dictionary<Type, List<Action<object>>>();

        private EventBus() { }
        
        public static EventBus Instance
        {
            get { return _instance ??= new EventBus(); }
        }
        public void AddListener<T>(Action<T> listener) where T : class
        {
            var eventType = typeof(T);
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = new List<Action<object>>();
            }
            _eventListeners[eventType].Add((obj) => listener(obj as T));
        }
        
        public void AddListener(Action listener)
        {
            var eventType = typeof(Action);
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = new List<Action<object>>();
            }
            _eventListeners[eventType].Add((obj) => listener());
        }
        
        public void RemoveListener<T>(Action<T> listener) where T : class
        {
            var eventType = typeof(T);
            if (_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType].Remove((obj) => listener(obj as T));
            }
        }
        
        public void RemoveListener(Action listener)
        {
            var eventType = typeof(Action);
            if (_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType].Remove((obj) => listener());
            }
        }
        
        public void Publish<T>(T eventData) where T : class
        {
            var eventType = typeof(T);
            if (!_eventListeners.ContainsKey(eventType)) return;
            foreach (var listener in _eventListeners[eventType])
            {
                listener?.Invoke(eventData);
            }
        }
        
        public void Publish(Type eventType)
        {
            if (!_eventListeners.ContainsKey(eventType)) return;
            foreach (var listener in _eventListeners[eventType])
            {
                listener?.Invoke(null);
            }
        }
    }
}
