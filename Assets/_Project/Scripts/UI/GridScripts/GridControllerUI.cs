using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.GridScripts
{
    public class GridControllerUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _panelGridTransform;

        private EventBinding<OnChangePanelEvent> _onChangePanelEvent;

        private EventBinding<OnClosePanelEvent> _onClosePanelEvent;

        private void OnEnable()
        {
            _onChangePanelEvent = new EventBinding<OnChangePanelEvent>(OnChangePanel);

            _onClosePanelEvent = new EventBinding<OnClosePanelEvent>(OnClosePanel);

            EventBus<OnChangePanelEvent>.Subscribe(_onChangePanelEvent);
            EventBus<OnClosePanelEvent>.Subscribe(_onClosePanelEvent);

            transform.DOScale(Vector3.one, 4f).SetEase(Ease.OutQuint);
        }

        private void OnDisable()
        {
            EventBus<OnChangePanelEvent>.Unsubscribe(_onChangePanelEvent);
            EventBus<OnClosePanelEvent>.Unsubscribe(_onClosePanelEvent);
        }

        private void OnChangePanel()
        {
            _panelGridTransform.DOScale(Vector3.one * 4f, .25f);
        }

        private void OnClosePanel()
        {
            _panelGridTransform.DOScale(Vector3.one, .25f);
        }
    }
}