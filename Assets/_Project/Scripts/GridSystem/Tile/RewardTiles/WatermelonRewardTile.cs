
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.SaveSystem;

namespace _Project.Scripts.GridSystem.Tile.RewardTiles
{
    public class WatermelonRewardTile : RewardTile
    {
        public override void GiveRewards()
        {
            EventBus<OnCollectedItemEvent>.Publish(new OnCollectedItemEvent{CollectedItemType = CollectedItemType.Watermelon});
        }
    }

}
