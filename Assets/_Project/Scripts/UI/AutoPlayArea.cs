using System;
using System.Threading;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random =UnityEngine.Random;

namespace _Project.Scripts.UI
{
    public class AutoPlayArea : MonoBehaviour
    {
        private bool _isToggleOn = false;
        [SerializeField]
        private RectTransform _toggleTransform;
        [SerializeField]
        private Button _toggleButton;
        [SerializeField]
        private Button _rollButton;
        
        [SerializeField]
        private TMP_InputField _moveAmountTextArea;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
            _toggleButton.onClick.AddListener(SetToggleButton);
        }

     

        private void SetToggleButton()
        {
            _isToggleOn = !_isToggleOn;

            _toggleTransform.DOAnchorPosX(_isToggleOn ? 75f : -75f, .15f);

            _rollButton.interactable = !_isToggleOn;
            
            EventBus<OnUpdateGameStateEvent>.Publish(new OnUpdateGameStateEvent
            {
                newState = _isToggleOn ? GameState.Auto : GameState.Normal
            });

            _moveAmountTextArea.interactable = !_isToggleOn;

            if (_isToggleOn)
            {
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }
                
                
                _cancellationTokenSource = new CancellationTokenSource();
                
                AutoMove(_cancellationTokenSource.Token,GetRollAmount()).Forget();
            }
            else
            {
                _cancellationTokenSource?.Cancel();
            }

        }

        private int GetRollAmount()
        {
            var num = _moveAmountTextArea.text;
     

            if (string.IsNullOrEmpty(num))
            {
                return 5;
            }
            
            return int.TryParse(num, out var parsedNum) ? parsedNum : Random.Range(3, 10);
            
        }

        private async UniTask AutoMove(CancellationToken token,int rollAmount)
        {
      
            try
            {
                for (var i = rollAmount; i > 0; i--)
                {
                    token.ThrowIfCancellationRequested();
                    
                    await UniTask.Delay(1500, cancellationToken: token);
                    
                    RollDice();

                    _moveAmountTextArea.text = i.ToString();
                    
                
                }
            }
            catch (OperationCanceledException)
            {
                // Auto movement canceled
              //  Debug.LogError("Auto Movement Canceled");
            }
            finally
            {
                OnAutoMoveCanceledOrCompleted();
            }
        }

        private void RollDice()
        {
            var number1 = Random.Range(1, 7);
            var number2 = Random.Range(1, 7);
            
            EventBus<RollDiceEvent>.Publish(new RollDiceEvent
            {
                Number1 = number1,
                Number2 = number2
            });
        }
        
        private void OnAutoMoveCanceledOrCompleted()
        {
         
            _isToggleOn = false;
            _toggleTransform.DOAnchorPosX(-75f, .15f);
            _rollButton.interactable = true;
            _moveAmountTextArea.interactable = true;
            _moveAmountTextArea.text = "";
            
            EventBus<OnUpdateGameStateEvent>.Publish(new OnUpdateGameStateEvent
            {
                newState = GameState.Normal
            });

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
