using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RollButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _textMeshes;

        public void OnButtonDown()
        {
            if (_textMeshes.Length < 2) return;
            var textChars1 = _textMeshes[0].text.ToCharArray();
            var textChars2 = _textMeshes[1].text.ToCharArray();

            if (textChars1.Length <= 0 || textChars2.Length <= 0 || !char.IsDigit(textChars1[0]) || !char.IsDigit(textChars2[0])) return;
                
                
            var number1 = int.Parse(textChars1[0].ToString());
            var number2 = int.Parse(textChars2[0].ToString());

            EventBus<RollDiceEvent>.Publish(new RollDiceEvent
            {
                Number1 = number1,
                Number2 = number2
            });
      
        
        }
    }
}
