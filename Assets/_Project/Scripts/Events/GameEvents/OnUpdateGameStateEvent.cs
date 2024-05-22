using _Project.Scripts.Events.EventBusScripts;

namespace _Project.Scripts.Events.GameEvents
{
    public class OnUpdateGameStateEvent : IEvent
    {
        public GameState newState;
    }
}
