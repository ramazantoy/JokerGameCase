using System;
using System.Collections.Generic;
using _Project.Scripts.UI.CollectItemScripts;
using UnityEngine;

namespace _Project.Scripts
{
    public class CollectItemHandler : MonoBehaviour
    {
        [SerializeField]
        private CollectItem _collectItemPref;
        private List<CollectItem> _collectItems;

        private void Awake()
        {
            _collectItems = new List<CollectItem>();

            for (int i = 0; i < 10; i++)
            {
                var tempItem = Instantiate(_collectItemPref, transform);
                _collectItems.Add(tempItem);
            }
        }

        private void AddItem(CollectItem collectItem)
        {
            collectItem.gameObject.SetActive(false);
            _collectItems.Add(collectItem);
        }
        
    }
}
