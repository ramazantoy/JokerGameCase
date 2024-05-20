using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {

        private EventBinding<OnBoardReady> _onboardReadyEvent;

        private void Awake()
        {
            _onboardReadyEvent = new EventBinding<OnBoardReady>((() =>
            {
                transform.DOJump(new Vector3(1.3f, 0f, -6.9f), 2f,1,.5f );
            }));
            
            EventBus<OnBoardReady>.Subscribe(_onboardReadyEvent);
        }


        private void OnDisable()
        {
            EventBus<OnBoardReady>.Unsubscribe(_onboardReadyEvent);
        }

    
    }
}
