using System;
using System.Collections.Generic;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.Funcs;
using _Project.Scripts.LeonsExtensions;
using _Project.Scripts.UI.CollectItemScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class CollectItemHandler : MonoBehaviour
    {
        [SerializeField] private CollectItem _collectItemPref;
        private List<CollectItem> _collectItems;

        private EventBinding<OnCollectedItemEvent> _onCollectedItemEvent;

        private void Awake()
        {
            _collectItems = new List<CollectItem>();

            for (int i = 0; i < 10; i++)
            {
                var tempItem = Instantiate(_collectItemPref, transform);
                _collectItems.Add(tempItem);
            }
        }

        private void OnEnable()
        {
            _onCollectedItemEvent = new EventBinding<OnCollectedItemEvent>(CollectItemAnim);

            EventBus<OnCollectedItemEvent>.Subscribe(_onCollectedItemEvent);
        }

        private void OnDisable()
        {
        }

        private void CollectItemAnim(OnCollectedItemEvent onCollectedItemEvent)
        {
            var type = onCollectedItemEvent.CollectedItemType;

            var collectedItemGroup = GameFuncs.GetCollectedItemGroup((int)type);

            var count = Random.Range(3, 5);

          var bornPoints=LeonsMath.GenerateRandomCirclePoints(onCollectedItemEvent.BornTransform, 2f, count, Camera.main);
          
          foreach (var bornPoint in bornPoints)
          {
              var item = GetItem();
            
              item.SetSprite((int)type);
              item.gameObject.SetActive(true);
              item.transform.position = bornPoint;
              item.MoveToTarget(collectedItemGroup.TargetTransform,AddItem);
          }
           
            
         
      
        }

        private void AddItem(CollectItem collectItem)
        {
            collectItem.gameObject.SetActive(false);
            _collectItems.Add(collectItem);

      
        }

        private CollectItem GetItem()
        {
            if (_collectItems.Count > 0)
            {
                return _collectItems.GetRandom(true);
            }

            return Instantiate(_collectItemPref, transform);
        }
    }
}