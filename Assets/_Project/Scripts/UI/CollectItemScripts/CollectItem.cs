using System;
using DG.Tweening;
using UnityEngine;

using UnityEngine.UI;
using Random = UnityEngine.Random;

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
            transform.SetParent(target);
            var moveTime = GameManager.GameState==GameState.Normal ? Random.Range(1f, 1.5f) : Random.Range(1f, 1.5f)/4f;
            transform.DOLocalMove(Vector3.zero, moveTime).OnComplete((() =>
            {
                onComplete?.Invoke(this);
            }));
        }
    }
}
