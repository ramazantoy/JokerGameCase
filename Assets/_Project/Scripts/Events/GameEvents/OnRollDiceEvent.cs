using _Project.Scripts.Events.EventBusScripts;

namespace _Project.Scripts.Events.GameEvents
{
    public struct OnRollDiceEvent : IEvent
    {
        public int Number1;
        public int Number2;
    }
}
