using System.Collections.Generic;

namespace _Project.Scripts.Events.EventBusScripts
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> _bindings = new HashSet<IEventBinding<T>>();


        public static void Subscribe(EventBinding<T> binding)
        {
        
            _bindings.Add(binding);
        }

        public static void Unsubscribe(EventBinding<T> binding)
        {
            _bindings.Remove(binding);
        }
        
        public static void Publish(T eventToRaise)
        {
    
            foreach(var binding in _bindings)
            {
                binding.OnEvent.Invoke(eventToRaise);
                binding.OnEventNoArgs.Invoke();
            }
        }

       public  static void Clear()
        {
            _bindings.Clear();
        }


     
    }
}
