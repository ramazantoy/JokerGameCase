using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.UI.CollectItemScripts
{
    
    [CreateAssetMenu(fileName = "CollectItemDataContainer", menuName = "ScriptableObjects/CollectItemDataContainer")]
    public class CollectItemDataContainer : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> _collectItemSprites;

        public Sprite GetSprite(int index)
        {
            return _collectItemSprites[index];
        }
    }
}
