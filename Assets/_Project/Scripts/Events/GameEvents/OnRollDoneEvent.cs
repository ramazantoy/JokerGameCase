
using _Project.Scripts.Events.EventBusScripts;

namespace _Project.Scripts.Events.GameEvents
{
    public struct OnRollDoneEvent : IEvent
    {
        public int rollValue;
    }
}
