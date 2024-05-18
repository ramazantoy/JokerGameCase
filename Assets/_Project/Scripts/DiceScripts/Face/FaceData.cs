using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.DiceScripts.Face
{
    [System.Serializable]
    public struct FaceData
    { 
        [SerializeField]
        private List<GameObject> _numbers;

        public void SetNumber(int index)
        {
            foreach (var face in _numbers)
            {
                face.SetActive(false);
            }
            
            _numbers[index-1].SetActive(true);
        }
    }
}
