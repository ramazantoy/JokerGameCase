using System;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Face
{
    public class DiceFace : MonoBehaviour
    {
        [SerializeField]
        private FaceData _properties;


        public void SetFaceNumber(int number)
        {
    
            _properties.SetNumber(number);
        }
    }
}
