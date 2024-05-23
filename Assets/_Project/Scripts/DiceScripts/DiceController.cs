using System;
using System.Collections.Generic;
using _Project.Scripts.DiceScripts.Controller;
using _Project.Scripts.Events;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.LeonsExtensions;
using UnityEngine;

namespace _Project.Scripts.DiceScripts
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] private DiceDataContainer _diceDataContainer;


        private List<Dice> _diceList;
        private EventBinding<OnRollDiceEvent> _rollDiceEvent;

        private readonly Dictionary<string, RollData> _rolledDicesDictionary = new Dictionary<string, RollData>();


        private void OnEnable()
        {
            _rollDiceEvent = new EventBinding<OnRollDiceEvent>(OnRollDices);

            EventBus<OnRollDiceEvent>.Subscribe(_rollDiceEvent);


            _diceList = new List<Dice>();
            for (var i = 0; i < 5; i++)
            {
                var diceTemp = Instantiate(_diceDataContainer.DicePref, transform);
                diceTemp.transform.localPosition = Vector3.zero;
                _diceList.Add(diceTemp);
            }
        }

        private void OnDisable()
        {
            EventBus<OnRollDiceEvent>.Unsubscribe(_rollDiceEvent);
        }

        private void OnRollDices(OnRollDiceEvent onRollDiceEvent)
        {
            var dice1 = GetDice();
            var dice2 = GetDice();
            
            dice1.gameObject.SetActive(true);
            dice2.gameObject.SetActive(true);

            var onRolledKey = Guid.NewGuid().ToString();
            
            dice1.RollDice(onRollDiceEvent.Number1, _diceDataContainer.GetRandomFace(),AddDice,onRolledKey);
            dice2.RollDice(onRollDiceEvent.Number2, _diceDataContainer.GetRandomFace(),AddDice,onRolledKey);

            var rolledList = new List<Dice> { dice1, dice2 };
            
          

            var rollData = new RollData(onRollDiceEvent.Number1 + onRollDiceEvent.Number2, rolledList);

            _rolledDicesDictionary.Add(onRolledKey,rollData);

         
        }

        private Dice GetDice()
        {
            if (_diceList.Count > 0)
            {
                return _diceList.GetRandom(true);
            }

            var diceTemp = Instantiate(_diceDataContainer.DicePref, transform);
            diceTemp.transform.localPosition = Vector3.zero;
            return diceTemp;
        }

        private void AddDice(Dice dice)
        {
            dice.gameObject.SetActive(false);
            _diceList.Add(dice);

            if (!_rolledDicesDictionary.TryGetValue(dice.OnCompleteKey, out var rollData)) return;
            
            rollData.RolledDices.Remove(dice);
            
            if (rollData.RolledDices.Count > 0) return;
            
            EventBus<OnRollDoneEvent>.Publish(new OnRollDoneEvent{rollValue = rollData.RollValue});
            _rolledDicesDictionary.Remove(dice.OnCompleteKey);
        }
    }
}