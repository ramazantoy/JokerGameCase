using System.Collections.Generic;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    [CreateAssetMenu(fileName = "AudioManagerDataContainer", menuName = "ScriptableObjects/AudioManagerDataContainer")]
    public class AudioManagerDataContainer : ScriptableObject
    {
        public GameSaveDataContainer GameSaveDataContainer;
        public AudioClip MainSound;
        public AudioClip CollectItemSound;
        public AudioClip OnPanelChangeSound;
        public List<AudioClip> DiceSounds;
    }
}
