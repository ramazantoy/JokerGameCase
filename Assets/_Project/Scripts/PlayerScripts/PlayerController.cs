using System.Collections.Generic;
using System.Threading;
using _Project.Scripts._ProjectExtensions;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.Funcs;
using _Project.Scripts.GridSystem.Tile;
using _Project.Scripts.SaveSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CollectedItemSaveDataContainer _collectedItemSaveDataContainer;

        private EventBinding<OnBoardReadyEvent> _onboardReadyEvent;
        private EventBinding<OnRollDoneEvent> _onRollDoneEvent;

        private RoadTileBase _currentRoadTile;
        private Queue<int> _movementQueue;
        private int _movementAmount;

        private int CurrentIndex => _currentRoadTile == null ? 0 : _currentRoadTile.TileIndex;
        
        private EventBinding<OnForceRebuildMapEvent> _onForceRebuildMapEvent;

        private readonly AsyncLock _movementLock = new AsyncLock();

        private CancellationTokenSource _cancellationToken;

        private void Awake()
        {
            _movementQueue = new Queue<int>();
            _currentRoadTile = null;
            _movementAmount = -1;

            _onForceRebuildMapEvent = new EventBinding<OnForceRebuildMapEvent>((() =>
            {
                _cancellationToken?.Cancel();
                _cancellationToken?.Dispose();
                
                transform.position = new Vector3(1.3f, 0f, -12.35f);
                transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }));

            _onboardReadyEvent = new EventBinding<OnBoardReadyEvent>((() =>
            {
                transform.DOJump(new Vector3(1.3f, 0f, -6.9f), 2f, 1, .5f);
                _movementQueue = new Queue<int>();
                _currentRoadTile = null;
                _movementAmount = -1;

                _cancellationToken = new CancellationTokenSource();
                Movement(_cancellationToken.Token).Forget();
            }));

            EventBus<OnBoardReadyEvent>.Subscribe(_onboardReadyEvent);
            EventBus<OnForceRebuildMapEvent>.Subscribe(_onForceRebuildMapEvent);

            _onRollDoneEvent = new EventBinding<OnRollDoneEvent>(OnRollDone);

            EventBus<OnRollDoneEvent>.Subscribe(_onRollDoneEvent);
        }
        

        private void OnDisable()
        {
            EventBus<OnBoardReadyEvent>.Unsubscribe(_onboardReadyEvent);
            EventBus<OnRollDoneEvent>.Unsubscribe(_onRollDoneEvent);
            EventBus<OnForceRebuildMapEvent>.Unsubscribe(_onForceRebuildMapEvent);
        }

        private async void OnRollDone(OnRollDoneEvent onRollDoneEvent)
        {
            using (await _movementLock.LockAsync())
            {
                _movementQueue.Enqueue(onRollDoneEvent.rollValue);
            }
        }

        private async UniTaskVoid Movement(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    await UniTask.WaitUntil((() => _movementQueue.Count > 0), cancellationToken: cancellationToken);

                    using (await _movementLock.LockAsync())
                    {
                        _movementAmount = _movementQueue.Dequeue();
                    }

                    while (_movementAmount > 0)
                    {
                    
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        _currentRoadTile = GameFuncs.GetRoadTile(CurrentIndex + 1);

                        var targetPosition = _currentRoadTile.transform.position + new Vector3(0, .2f, 0f);

                        var directionToTarget = GameFuncs.GetRoadTile(CurrentIndex + 1).transform.position - transform.position;
                        var lookRotation = Quaternion.LookRotation(directionToTarget).eulerAngles;

                        lookRotation.y -= 90f;
                        lookRotation.x = 0;
                        lookRotation.z = 0;

                        GameFuncs.GetRoadTile(CurrentIndex).PlayParticle();

                        await UniTask.WhenAll(transform.DOJump(targetPosition, 1.5f, 1, .35f / (GameManager.GameState == GameState.Normal ? 1f : 4f)).ToUniTask(cancellationToken: cancellationToken));

                        if (_currentRoadTile != null)
                        {
                            var reward = _currentRoadTile.GiveRewards();

                            if (reward.Item1 != CollectedItemType.None)
                            {
                                _collectedItemSaveDataContainer.Data.SaveDictionary[reward.Item1] += reward.Item2;
                            }

                        }
                

                        transform.rotation = Quaternion.Euler(lookRotation);
                        _movementAmount--;
                    }
                }
            }
            catch
            {
                //Movement Canceled
            }
      
        }
    }
}