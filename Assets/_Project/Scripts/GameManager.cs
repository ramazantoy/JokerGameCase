using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {

        private EventBinding<OnUpdateGameStateEvent> _onUpdateGameStateEvent;
        public static GameState GameState { get; private set; }

        private void Awake()
        {
            GameState= GameState.Normal;
        }

        private void OnEnable()
        {
            _onUpdateGameStateEvent = new EventBinding<OnUpdateGameStateEvent>(UpdateGameState);
            
            EventBus<OnUpdateGameStateEvent>.Subscribe(_onUpdateGameStateEvent);
        }

        private void OnDisable()
        {
            EventBus<OnUpdateGameStateEvent>.Unsubscribe(_onUpdateGameStateEvent);
        }


        private void UpdateGameState(OnUpdateGameStateEvent onUpdateGameStateEvent)
        {
            GameState = onUpdateGameStateEvent.newState;
        }

        private void OnApplicationQuit()
        {
            EventBus<IEvent>.Clear();
        }
    }
}