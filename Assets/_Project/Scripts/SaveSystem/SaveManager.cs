using System;
using System.Collections.Generic;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private List<SavableData> _savableDatas;


        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            SaveSystem.LoadFromFile();

            foreach (var savableData in _savableDatas)
            {
                savableData.Load();
            }
            
            EventBus<OnGameDataLoadedEvent>.Publish(new OnGameDataLoadedEvent());
        }

        private void Save()
        {
            foreach (var savableData in _savableDatas)
            {
                savableData.Save();
            }

            SaveSystem.SaveToFile();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application focus");
#endif
        }

        private void OnApplicationQuit()
        {
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application quit");
#endif
        }

   
    }
}