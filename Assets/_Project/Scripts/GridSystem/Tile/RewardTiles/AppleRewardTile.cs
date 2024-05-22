
using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public class AppleRewardTile : RewardTile
    {
        public override void GiveRewards()
        {
         EventBus<OnCollectedItemEvent>.Publish(new OnCollectedItemEvent{CollectedItemType = CollectedItemType.Apple});
        }
    }
}
