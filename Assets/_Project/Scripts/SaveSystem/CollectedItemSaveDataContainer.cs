
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    [CreateAssetMenu(fileName = "CollectItemSaveDataContainer", menuName = "ScriptableObjects/CollectItemSaveDataContainer")]
    public class CollectedItemSaveDataContainer : SavableData
    {
        public CollectedItemSave Data;
        public override void Save()
        {
            Data.Save(Id);
        }

        public override void Load()
        {
            Data = SaveDataHelper.Load(Id, new CollectedItemSave());
        }
    }
    
    [System.Serializable]
    public class CollectedItemSave
    {
        public Dictionary<CollectedItemType, int> SaveDictionary;
        
        public CollectedItemSave()
        {
            SaveDictionary=new Dictionary<CollectedItemType, int>
            {
                { CollectedItemType.Apple, 0 },
                { CollectedItemType.Banana, 0 },
                { CollectedItemType.Watermelon, 0 }
            };

        }
      

    }
    public enum CollectedItemType
    {
        None=-1,
        Apple=0,
        Banana=1,
        Watermelon=2,
    }
}
