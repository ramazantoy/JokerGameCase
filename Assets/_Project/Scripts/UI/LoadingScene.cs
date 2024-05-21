using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
        private EventBinding<OnGameDataLoadedEvent> _onGameDataLoadedEvent;

        private void Awake()
        {
            _onGameDataLoadedEvent = new EventBinding<OnGameDataLoadedEvent>(BlowYourSelf);
            
            EventBus<OnGameDataLoadedEvent>.Subscribe(_onGameDataLoadedEvent);
        }

        private void BlowYourSelf()
        { 
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            EventBus<OnGameDataLoadedEvent>.Unsubscribe(_onGameDataLoadedEvent);
        }
    }
}
