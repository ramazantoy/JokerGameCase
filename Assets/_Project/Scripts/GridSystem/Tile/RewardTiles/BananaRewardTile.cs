

using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public class BananaRewardTile : RewardTile
    {
        public override (CollectedItemType, int)  GiveRewards()
        {
            EventBus<OnCollectedItemEvent>.Publish(new OnCollectedItemEvent{CollectedItemType = CollectedItemType.Banana,BornTransform=transform});
            return (CollectedItemType.Banana, RewardCount);
        }
    }
}
