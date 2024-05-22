using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile
{
    public  abstract class TileBase : MonoBehaviour
    {
        public Vector3 StartScale;
        public abstract void SetText(string text);

        public void StartAnim(float time)
        {
            transform.DOScale(StartScale, time);
        }
    }
}
