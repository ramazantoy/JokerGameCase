using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.UI.CollectItemScripts
{
    public class CollectItem : MonoBehaviour
    {
        [SerializeField]
        private CollectItemDataContainer _collectItemDataContainer;
        private Image _myImage;

        private void Awake()
        {
            _myImage = transform.GetComponent<Image>();
        }

        private void SetSprite(int index)
        {
            _myImage.sprite = _collectItemDataContainer.GetSprite(index);
        }

        public void MoveToTarget(RectTransform target,Action<CollectItem> onComplete)
        {
            transform.DOLocalMove(Vector3.zero, .25f).OnComplete((() =>
            {
                onComplete?.Invoke(this);
            }));
        }
    }
}
