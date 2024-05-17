using System;

namespace _Project.Scripts.Handlers
{
    public static class ActionHandler
    {
        public static Action<float> FixedUpdateEvent;
        public static Action<float> UpdateEvent;
        
        //Game Values Changed
        public static Action<int> EarnGemChanged;
        public static Action<int> EarnMoneyChanged;
        public static Action<bool> SoundSettingsChanged;
        public static Action NoAdsBought;
        public static Action<bool> HapticSettingsChanged;
        public static Action<int> EarnMoneyUp;
        


    }

}