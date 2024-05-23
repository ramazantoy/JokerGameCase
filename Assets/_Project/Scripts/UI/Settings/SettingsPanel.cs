using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Settings
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private SettingsPanelDataContainer _settingsPanelDataContainer;

        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private GameObject _panelObj;

        [SerializeField] private Button _soundEffectButton;
        [SerializeField] private Button _musicButton;

        [SerializeField] private Button _reBuildButton;
        [SerializeField] private Image _reBuildImg;


        private void OnEnable()
        {
            _openButton.onClick.AddListener(OpenPanel);
            _closeButton.onClick.AddListener(ClosePanel);
            _soundEffectButton.onClick.AddListener(SoundEffectButtonDown);
            _musicButton.onClick.AddListener(MusicButtonDown);
            _reBuildButton.onClick.AddListener(ReBuildButtonDown);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
            _reBuildButton.onClick.RemoveAllListeners();
        }

        private void OpenPanel()
        {
            EventBus<OnChangePanelEvent>.Publish(new OnChangePanelEvent());
            SetSprites();
            _panelObj.SetActive(true);
        }

        private void ClosePanel()
        {
            EventBus<OnClosePanelEvent>.Publish(new OnClosePanelEvent());
            _panelObj.SetActive(false);
        }

        private void SoundEffectButtonDown()
        {
            _settingsPanelDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn =
                !_settingsPanelDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn;
            _soundEffectButton.image.sprite = _settingsPanelDataContainer.GetSprite(0,
                _settingsPanelDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn);
        }

        private void MusicButtonDown()
        {
            _settingsPanelDataContainer.GameSaveDataContainer.Data.IsMusicOn =
                !_settingsPanelDataContainer.GameSaveDataContainer.Data.IsMusicOn;
            _musicButton.image.sprite = _settingsPanelDataContainer.GetSprite(1,
                _settingsPanelDataContainer.GameSaveDataContainer.Data.IsMusicOn);
            EventBus<OnMusicSettingsChangedEvent>.Publish(new OnMusicSettingsChangedEvent());
        }

        private void SetSprites()
        {
            _soundEffectButton.image.sprite = _settingsPanelDataContainer.GetSprite(0,
                _settingsPanelDataContainer.GameSaveDataContainer.Data.IsSoundEffectsOn);
            _musicButton.image.sprite = _settingsPanelDataContainer.GetSprite(1,
                _settingsPanelDataContainer.GameSaveDataContainer.Data.IsMusicOn);
        }

        private void ReBuildButtonDown()
        {
            EventBus<OnForceRebuildMapEvent>.Publish(new OnForceRebuildMapEvent());
            _reBuildButton.interactable = false;
            _reBuildImg.fillAmount = 0;
            _reBuildImg.DOFillAmount(1, 3f).OnComplete((() =>
            {
                _reBuildButton.interactable = true;
            }));

        }
    }
}