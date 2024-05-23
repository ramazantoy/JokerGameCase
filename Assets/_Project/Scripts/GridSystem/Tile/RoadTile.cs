using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile
{
    public class RoadTile : RoadTileBase
    {
        public override (CollectedItemType, int) GiveRewards()
        {
            return (CollectedItemType.None, 0);
        }
    }
}
