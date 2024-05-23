using System;
using _Project.Scripts.SaveSystem;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public abstract class RewardTile : RoadTileBase
    {

        protected int RewardCount;

        private void Awake()
        {
            RewardCount = Random.Range(3, 8);
        }

        public abstract override (CollectedItemType, int) GiveRewards();

    }
}
