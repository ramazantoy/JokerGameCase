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
        private EventBinding<RollDiceEvent> _rollDiceEvent;


        private void OnEnable()
        {
            _rollDiceEvent = new EventBinding<RollDiceEvent>(OnRollDices);

            EventBus<RollDiceEvent>.Subscribe(_rollDiceEvent);


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
            EventBus<RollDiceEvent>.Unsubscribe(_rollDiceEvent);
        }

        private void OnRollDices(RollDiceEvent rollDiceEvent)
        {
            var dice1 = GetDice();
            var dice2 = GetDice();
            
            dice1.gameObject.SetActive(true);
            dice2.gameObject.SetActive(true);
            
            dice1.RollDice(rollDiceEvent.Number1, _diceDataContainer.GetRandomFace(),AddDice);
            dice2.RollDice(rollDiceEvent.Number2, _diceDataContainer.GetRandomFace(),AddDice);
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
        }
    }
}