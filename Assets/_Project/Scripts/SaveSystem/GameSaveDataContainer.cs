using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    [CreateAssetMenu(fileName = "GameSaveData",menuName = "ScriptableObjects/GameSaveData")]
    public  class GameSaveDataContainer : SavableData
    {
        public GameSaveData Data;
        public override void Save()
        {
            Data.Save(Id);
        }

        public override void Load()
        {
            Data = SaveDataHelper.Load(Id, new GameSaveData());
        }
    
    }


    [System.Serializable]
    public class GameSaveData
    {
        private int _earnMoney;
        private int _earnGem;
        private int _lastLevel;
        private bool _noAds;
        private string _lastSessionTime;
        private bool _isVibrationOn;
        private bool _isMusicOn;


        public GameSaveData()
        {
            _lastLevel = 1;
            _isVibrationOn = true;
            _isMusicOn = true;
            _earnMoney = 0;
            _earnGem = 0;
            _noAds = false;
        }
    
    
        public int EarnGem
        {
            get => _earnGem;
            set
            {
                _earnGem = value;
                ActionHandler.EarnGemChanged?.Invoke(_earnGem);
            }
        }

        public int EarnMoney
        {
            get => _earnMoney;
            set
            {
                _earnMoney = value;
                ActionHandler.EarnMoneyChanged?.Invoke(_earnMoney);
 
            }
        }

        public bool IsMusicOn
        {
            get => _isMusicOn;
            set
            {
                _isMusicOn = value;
                
                ActionHandler.SoundSettingsChanged?.Invoke(!value);
            }
        }

        public int LastLevel
        {
            get => _lastLevel;
            set => _lastLevel = value;
        }

    

        public bool NoAds
        {
            get => _noAds;
            set
            {
                if (value)
                {
                    ActionHandler.NoAdsBought?.Invoke();
                }
                _noAds = value;
            }
        }

        public string LastSessionTime
        {
            get => _lastSessionTime;
            set => _lastSessionTime = value;
        }
        

        public bool IsVibrationOn
        {
            get => _isVibrationOn;
            set
            {
                _isVibrationOn = value;
                ActionHandler.HapticSettingsChanged?.Invoke(value);
            }
        }
    
    
    }
}