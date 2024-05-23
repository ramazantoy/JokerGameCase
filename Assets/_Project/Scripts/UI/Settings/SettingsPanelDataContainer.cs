using System.Collections.Generic;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.UI.Settings
{
    [CreateAssetMenu(fileName = "SettingsPanelDataContainer",menuName = "ScriptableObjects/SettingsPanelDataContainer")]
    public class SettingsPanelDataContainer : ScriptableObject
    {
        [SerializeField]
        public GameSaveDataContainer GameSaveDataContainer;
        [SerializeField]
        private List<SettingItemSprite> _settingItemSprites;

        public Sprite GetSprite(int id, bool value)
        {
            return _settingItemSprites[id].GetSprite(value);
        }
    }

    [System.Serializable]
    public struct SettingItemSprite
    {
        [SerializeField]
        private Sprite _enabled;
        [SerializeField]
        private Sprite _disabled;
      

        public Sprite GetSprite(bool value)
        {
            return value ? _enabled : _disabled;
        }
    }
}
