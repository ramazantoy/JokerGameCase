using System.Collections.Generic;
using _Project.Scripts.LeonsExtensions;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Controller
{ 
   
    [CreateAssetMenu(fileName = "DiceDataContainer",menuName = "ScriptableObjects/DiceDataContainer")]
    public class DiceDataContainer : ScriptableObject
    {
        [SerializeField]
        private List<FaceIndexData> _faceIndexData;

        public FaceIndexData GetRandomFace()
        {
            return _faceIndexData.GetRandom();
        }
    }

    [System.Serializable]
    public struct FaceIndexData
    {
        public string AnimName;
        public int FaceIndex;
    }
}
