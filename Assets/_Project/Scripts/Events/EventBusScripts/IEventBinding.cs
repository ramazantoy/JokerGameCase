using System;

namespace _Project.Scripts.Events.EventBusScripts
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}