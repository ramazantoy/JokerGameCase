using System;
using System.Collections.Generic;
using _Project.Scripts.DiceScripts.Face;
using _Project.Scripts.LeonsExtensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.DiceScripts.Controller
{
    public class DiceController : MonoBehaviour
    {
        [SerializeField] 
        private DiceControllerData _properties;


        private void OnEnable()
        {
            
        }

        private void RollDice(int number)
        {
            List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };


            int[] oppositeIndices = { 5, 2, 1, 4, 3, 0 };

            var faceIndexData = _properties.DiceDataContainer.GetRandomFace();

            _properties.DiceFaces[faceIndexData.FaceIndex].SetFaceNumber(number);
            _properties.DiceFaces[oppositeIndices[faceIndexData.FaceIndex]].SetFaceNumber(7 - number);
            numbers.Remove(number);
            numbers.Remove(7 - number);

            var filledIndices = new HashSet<int> { faceIndexData.FaceIndex, oppositeIndices[faceIndexData.FaceIndex] };

            var i = 0;
            while (filledIndices.Count < _properties.DiceFaces.Count)
            {
                if (filledIndices.Contains(i))
                {
                    i++;
                    continue;
                }

                var faceNumber = numbers[0];
                var oppositeFaceNumber = 7 - faceNumber;

                var oppositeIndex = oppositeIndices[i];
                if (!filledIndices.Contains(oppositeIndex))
                {
                    _properties.DiceFaces[i].SetFaceNumber(faceNumber);
                    _properties.DiceFaces[oppositeIndex].SetFaceNumber(oppositeFaceNumber);
                    numbers.Remove(faceNumber);
                    numbers.Remove(oppositeFaceNumber);

                    filledIndices.Add(i);
                    filledIndices.Add(oppositeIndex);
                }

                i++;
            }

            _properties.DiceAnimator.Play(faceIndexData.AnimName, -1, 0);
        }
    }
}