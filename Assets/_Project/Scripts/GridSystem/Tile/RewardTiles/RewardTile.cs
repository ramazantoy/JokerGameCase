using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public abstract class RewardTile : RoadTileBase
    {
        public abstract override void GiveRewards();

    }
}
