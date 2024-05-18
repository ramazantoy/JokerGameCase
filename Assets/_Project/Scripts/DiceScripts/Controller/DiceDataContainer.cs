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
        
        private Queue<FaceIndexData> _shuffledFaces;

        public FaceIndexData GetRandomFace()
        {
            if (_shuffledFaces == null || _shuffledFaces.Count == 0)
            {
                ShuffleFaces();
            }

            return _shuffledFaces.Dequeue();
        }

        private void ShuffleFaces()
        {
            List<FaceIndexData> shuffledList = new List<FaceIndexData>(_faceIndexData);
            int count = shuffledList.Count;
            while (count > 1)
            {
                count--;
                int index = Random.Range(0, count + 1);
                FaceIndexData temp = shuffledList[index];
                shuffledList[index] = shuffledList[count];
                shuffledList[count] = temp;
            }
            
            _shuffledFaces = new Queue<FaceIndexData>(shuffledList);
        }
    }

    [System.Serializable]
    public struct FaceIndexData
    {
        public string AnimName;
        public int FaceIndex;
    }
}
