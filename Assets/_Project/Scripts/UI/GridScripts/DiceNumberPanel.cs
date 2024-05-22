using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.GridScripts
{
    public class DiceNumberPanel : MonoBehaviour
    {
        
        [SerializeField]
        private TMP_InputField[] _tmpInputFields;

        private EventBinding<OnUpdateGameStateEvent> _onUpdateGameStateEvent;
        private void OnEnable()
        {
            _onUpdateGameStateEvent = new EventBinding<OnUpdateGameStateEvent>(OnUpdateGameStateEvent);
            
            EventBus<OnUpdateGameStateEvent>.Subscribe(_onUpdateGameStateEvent);
        }

        private void OnDisable()
        {
            EventBus<OnUpdateGameStateEvent>.Unsubscribe(_onUpdateGameStateEvent);
        }

        private void OnUpdateGameStateEvent(OnUpdateGameStateEvent onUpdateGameStateEvent)
        {
            transform.DOKill();
            transform.DOScale(onUpdateGameStateEvent.newState == GameState.Normal ? Vector3.one : Vector3.one * 3f,
                .25f);
            
            foreach (var tmpInputField in _tmpInputFields)
            {
                tmpInputField.text = "";
                tmpInputField.interactable = onUpdateGameStateEvent.newState == GameState.Normal;
            }

        }
    }
}
