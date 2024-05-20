using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.UI
{
    public class RollButton : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private Button _rollButton;

        private void OnEnable()
        {
            _rollButton.onClick.AddListener(OnButtonDown);
        }

        private void OnDisable()
        {
            _rollButton.onClick.RemoveAllListeners();
        }

        [SerializeField] private TextMeshProUGUI[] _textMeshes;

        public void OnButtonDown()
        {
            if (_textMeshes.Length < 2) return;
            var textChars1 = _textMeshes[0].text.ToCharArray();
            var textChars2 = _textMeshes[1].text.ToCharArray();

            var number1 = Random.Range(1, 7);
            var number2 = Random.Range(1, 7);
            
            if (textChars1.Length > 0 && textChars2.Length > 0 && char.IsDigit(textChars1[0]) && char.IsDigit(textChars2[0]))
            {
                number1 = int.Parse(textChars1[0].ToString());
                number2 = int.Parse(textChars2[0].ToString());
            }

            _rollButton.interactable = false;
            _fillImage.fillAmount = 0;

            _fillImage.DOFillAmount(1f, 2f).OnComplete((() =>
            {
                _rollButton.interactable = true;
            }));

            EventBus<RollDiceEvent>.Publish(new RollDiceEvent
            {
                Number1 = number1,
                Number2 = number2
            });
        }
    }
}