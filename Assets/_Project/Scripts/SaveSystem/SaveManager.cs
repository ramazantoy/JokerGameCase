using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameSaveDataContainer _gameSaveDataContainer;

        [SerializeField] private List<SavableData> _savableDatas;
        private GameSaveData GameSaveData => _gameSaveDataContainer.Data;


        private void Awake()
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

            GameSaveData.LastSessionTime = DateTime.Now.ToBinary().ToString();
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application focus");
#endif
        }

        private void OnApplicationQuit()
        {
            GameSaveData.LastSessionTime = DateTime.Now.ToBinary().ToString();
            Save();

#if UNITY_EDITOR
            Debug.LogWarning("Data saved on application quit");
#endif
        }

   
    }
}