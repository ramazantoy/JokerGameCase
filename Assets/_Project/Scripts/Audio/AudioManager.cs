using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.LeonsExtensions;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioManagerDataContainer _audioManagerDataContainer;

        [SerializeField]
        private AudioSource _audioSource;

        private EventBinding<OnRollDiceEvent> _onRollDiceEvent;
        private EventBinding<OnCollectedItemEvent> _onCollectItemEvent;
        private EventBinding<OnPlayClickSoundEvent> _onPlayClickSoundEvent;

        private void Start()
        {
            _audioSource.clip = _audioManagerDataContainer.MainSound;
      
            SetMusicSettings();
        }

        private void OnEnable()
        {
            _onRollDiceEvent = new EventBinding<OnRollDiceEvent>(PlayRollDiceSound);
            _onCollectItemEvent = new EventBinding<OnCollectedItemEvent>(PlayCollectItemSound);
            _onPlayClickSoundEvent = new EventBinding<OnPlayClickSoundEvent>(PlayOnClickSound);
       
            
            EventBus<OnRollDiceEvent>.Subscribe(_onRollDiceEvent);
            EventBus<OnCollectedItemEvent>.Subscribe(_onCollectItemEvent);
            EventBus<OnPlayClickSoundEvent>.Subscribe(_onPlayClickSoundEvent);


        }

        private void OnDisable()
        {
            EventBus<OnRollDiceEvent>.Unsubscribe(_onRollDiceEvent);
            EventBus<OnCollectedItemEvent>.Unsubscribe(_onCollectItemEvent);
            EventBus<OnPlayClickSoundEvent>.Unsubscribe(_onPlayClickSoundEvent);
         
        }

        private void SetMusicSettings()
        {
            if (_audioManagerDataContainer.GameSaveDataContainer.Data.IsMusicOn)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
        private void PlayRollDiceSound()
        {
           if(!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return; 
           
           _audioSource.PlayOneShot(_audioManagerDataContainer.DiceSounds.GetRandom());
        }

        private void PlayCollectItemSound()
        {
            if(!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return; 
            
            _audioSource.PlayOneShot(_audioManagerDataContainer.CollectItemSound);
        }

        private void PlayOnClickSound()
        {
            if(!_audioManagerDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn) return;
            _audioSource.PlayOneShot(_audioManagerDataContainer.OnPanelChangeSound);
        }
    }
}
