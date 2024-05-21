using System;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        private EventBinding<OnGameDataLoadedEvent> _onGameDataLoadedEvent;
        private void Awake()
        {
            _onGameDataLoadedEvent = new EventBinding<OnGameDataLoadedEvent>(LoadScene);
            
            EventBus<OnGameDataLoadedEvent>.Subscribe(_onGameDataLoadedEvent);
        }


        private void OnDisable()
        {
            EventBus<OnGameDataLoadedEvent>.Unsubscribe(_onGameDataLoadedEvent);
        }

        private void LoadScene()
        { 
            LoadSceneAdditiveAsync(SceneType.GamePlay).Forget();   
        }

        private async UniTaskVoid LoadSceneAdditiveAsync(SceneType sceneType)
        {
            
            await UniTask.WaitForSeconds(Random.Range(1, 3f)); 
            
            SceneManager.LoadSceneAsync((int)sceneType, LoadSceneMode.Additive);
       
       
            Debug.LogWarning("scene load called");
        }
        
    }


    public enum SceneType
    {
        Null = -1,
        Main = 0,
        GamePlay=1,


    }
}