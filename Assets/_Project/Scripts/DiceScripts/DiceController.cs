using System;
using _Project.Scripts.DiceScripts.Controller;
using _Project.Scripts.Events;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;

namespace _Project.Scripts.DiceScripts
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private DiceDataContainer _diceDataContainer;
        [SerializeField] private Dice[] _diceObjects;

        private EventBinding<RollDiceEvent> _rollDiceEvent;

   

        private void OnEnable()
        {
           _rollDiceEvent = new EventBinding<RollDiceEvent>(OnRollDices);
           
           EventBus<RollDiceEvent>.Subscribe(_rollDiceEvent);

        }
        
        private void OnDisable()
        {
            EventBus<RollDiceEvent>.Unsubscribe(_rollDiceEvent);
        }

        private void OnRollDices(RollDiceEvent rollDiceEvent)
        {
            _diceObjects[0].RollDice(rollDiceEvent.Number1, _diceDataContainer.GetRandomFace());
            _diceObjects[1].RollDice(rollDiceEvent.Number2, _diceDataContainer.GetRandomFace());
        }
        
    }
}