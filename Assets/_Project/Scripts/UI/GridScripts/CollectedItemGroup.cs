using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.GridScripts
{
    public sealed class CollectedItemGroup : MonoBehaviour
    {

        [SerializeField]
        private CollectedItemSaveDataContainer _collectedItemSaveDataContainer;
        
        [SerializeField]
        private TextMeshProUGUI _collectedAmountText;
        [SerializeField]
        private CollectedItemType _collectedItemType;

        private EventBinding<OnCollectedItemEvent> _onCollectedItemEvent;

        [SerializeField]
        private RectTransform _targetTransform;

        public RectTransform TargetTransform => _targetTransform;

        public void Awake()
        {
          SetText();
        }

        public void OnEnable()
        {

            _onCollectedItemEvent = new EventBinding<OnCollectedItemEvent>(SetText);
            
            EventBus<OnCollectedItemEvent>.Subscribe(_onCollectedItemEvent);
        }

        private void OnDisable()
        {
            EventBus<OnCollectedItemEvent>.Unsubscribe(_onCollectedItemEvent);
        }

        private void SetText()
        {
            _collectedAmountText.text = _collectedItemSaveDataContainer.Data.SaveDictionary[_collectedItemType].ToString();
        }
    }
}
