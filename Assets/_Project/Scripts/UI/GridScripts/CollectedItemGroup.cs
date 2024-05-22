using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.GridScripts
{
    public class CollectedItemGroup : MonoBehaviour
    {

        [SerializeField]
        private CollectedItemSaveDataContainer _collectedItemSaveDataContainer;
        [SerializeField]
        private TextMeshProUGUI _collectedAmountText;
        private CollectedItemType _collectedItemType;

        private EventBinding<OnCollectedItemEvent> _onCollectedItemEvent;

        public virtual void Awake()
        {
          SetText();
        }

        public virtual void OnEnable()
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
