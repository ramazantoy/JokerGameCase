using System;
using DG.Tweening;
using UnityEngine;

using UnityEngine.UI;

namespace _Project.Scripts.UI.CollectItemScripts
{
    public class CollectItem : MonoBehaviour
    {
        [SerializeField]
        private CollectItemDataContainer _collectItemDataContainer;
        
        [SerializeField]
        private Image _myImage;

    

        public void SetSprite(int index)
        {
            _myImage.sprite = _collectItemDataContainer.GetSprite(index);
        }

        public void MoveToTarget(RectTransform target,Action<CollectItem> onComplete)
        {
            transform.parent = target;
            transform.DOLocalMove(Vector3.zero, .35f).OnComplete((() =>
            {
                onComplete?.Invoke(this);
            }));
        }
    }
}
