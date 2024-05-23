using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _openButton;
        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private GameObject _panelObj;
        private void OnEnable()
        {
            _openButton.onClick.AddListener(OpenPanel);
            _closeButton.onClick.AddListener(ClosePanel);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void OpenPanel()
        {
            EventBus<OnChangePanelEvent>.Publish(new OnChangePanelEvent());
            _panelObj.SetActive(true);
        }

        private void ClosePanel()
        {
            EventBus<OnClosePanelEvent>.Publish(new OnClosePanelEvent());
            _panelObj.SetActive(false);
        }
    }
}
