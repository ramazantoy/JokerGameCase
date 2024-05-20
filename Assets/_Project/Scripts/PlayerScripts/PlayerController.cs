using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.DiceScripts.Controller;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.Funcs;
using _Project.Scripts.GridSystem.Tile;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {

        private EventBinding<OnBoardReadyEvent> _onboardReadyEvent;

        private EventBinding<OnRollDoneEvent> _onRollDoneEvent;


        private RoadTile _currentRoadTile;
        private Queue<List<RoadTile>> _movementQueue;
        private List<RoadTile> _currentMovementList;

        private int CurrentIndex => _currentRoadTile == null ? 0 : _currentRoadTile.TileIndex;


        private void Awake()
        {
            _movementQueue = new Queue<List<RoadTile>>();
            
            _onboardReadyEvent = new EventBinding<OnBoardReadyEvent>((() =>
            {
                transform.DOJump(new Vector3(1.3f, 0f, -6.9f), 2f,1,.5f );
            }));
            
            EventBus<OnBoardReadyEvent>.Subscribe(_onboardReadyEvent);

            _onRollDoneEvent = new EventBinding<OnRollDoneEvent>(OnRollDone);
            
            EventBus<OnRollDoneEvent>.Subscribe(_onRollDoneEvent);

  
        }

        private void Start()
        {
          Movement().Forget();
        }


        private void OnDisable()
        {
            EventBus<OnBoardReadyEvent>.Unsubscribe(_onboardReadyEvent);
            EventBus<OnRollDoneEvent>.Unsubscribe(_onRollDoneEvent);
        }

        private void OnRollDone(OnRollDoneEvent onRollDoneEvent)
        {
            _movementQueue.Enqueue( GameFuncs.GetMovementTiles(CurrentIndex,onRollDoneEvent.rollValue));
        }


        private async UniTaskVoid Movement()
        {
            while (true)
            {
                await UniTask.WaitUntil((() => _movementQueue.Count > 0));
                
                _currentMovementList = _movementQueue.Dequeue();
             
                while (_currentMovementList.Count > 0)
                {
                    _currentRoadTile = _currentMovementList[0];
                    _currentMovementList.RemoveAt(0);
                
                    
                    var targetPosition = _currentRoadTile.transform.position + new Vector3(0, .2f, 0f);
                    
                    var directionToTarget = GameFuncs.GetRoadTile(CurrentIndex+1).transform.position - transform.position;
                    var lookRotation = Quaternion.LookRotation(directionToTarget).eulerAngles;

                    lookRotation.y -= 90f;
                    lookRotation.x = 0; lookRotation.z = 0;

                    GameFuncs.GetRoadTile(CurrentIndex).PlayParticle();
                    await UniTask.WhenAll(transform.DOJump(targetPosition, 1.5f, 1, .35f).ToUniTask());
                    
       
                    
                    transform.rotation = Quaternion.Euler(lookRotation);
                    
                }
                
            }
            
        }
    
    }
}