using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public abstract class RewardTile : RoadTileBase
    {
        public Color TileColor;

        public void Awake()
        {
            transform.GetComponent<MeshRenderer>().material.DOColor(TileColor, 0f);
        }

        public abstract override void GiveRewards();

    }
}
