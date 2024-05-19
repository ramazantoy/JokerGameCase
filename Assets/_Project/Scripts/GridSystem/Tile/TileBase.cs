using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile
{
    public  abstract class TileBase : MonoBehaviour
    {
        public Vector2Int TileCoordinate { get; set; }
        public abstract void SetText(string text);
    }
}
