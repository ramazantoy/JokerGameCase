using _Project.Scripts.Events.EventBusScripts;

namespace _Project.Scripts.Events.GameEvents
{
    public struct RollDiceEvent : IEvent
    {
        public int Number1;
        public int Number2;
    }
}
