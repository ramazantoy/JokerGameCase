using System;
using _Project.Scripts.DiceScripts.Controller;
using _Project.Scripts.Events;
using UnityEngine;
using UnityEngine.Serialization;
using EventType = _Project.Scripts.Events.EventType;

namespace _Project.Scripts.DiceScripts
{
   public class DiceController : MonoBehaviour
   { 
      [SerializeField]
      private DiceDataContainer _diceDataContainer;
      [SerializeField]
      private Dice[] _diceObjects;

      private void OnEnable()
      {
         EventBus.Instance.AddListener<int,int>(EventType.RollDice, OnRollDices);
      }

    

      private void OnRollDices(int num1,int num2)
      {
         
         _diceObjects[0].RollDice(num1,_diceDataContainer.GetRandomFace());
         _diceObjects[1].RollDice(num2,_diceDataContainer.GetRandomFace());
      }
   }
}
