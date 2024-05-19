using TMPro;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile
{
    public class RoadTile : TileBase
    {
        public TextMeshPro TileText;
        public override void SetText(string text)
        {
            TileText.text = text;
        }
    }
}
