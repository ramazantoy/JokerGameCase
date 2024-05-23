
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public class AppleRewardTile : RewardTile
    {
        public override (CollectedItemType, int) GiveRewards()
        {
         EventBus<OnCollectedItemEvent>.Publish(new OnCollectedItemEvent{CollectedItemType = CollectedItemType.Apple,BornTransform = transform});
         return (CollectedItemType.Apple, RewardCount);
        }
    }
}
