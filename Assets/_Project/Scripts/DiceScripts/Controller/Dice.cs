using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.DiceScripts.Face;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Controller
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] 
        private DiceData _properties;

        private Action<Dice> _onRollDone;
        
        
        public string OnCompleteKey { get; private set; }
        

      
        public void RollDice(int number, FaceIndexData faceIndex,Action<Dice> onRollDone,string onCompleteKey)
        {
            List<int> numbers = new() { 1, 2, 3, 4, 5, 6 };


            int[] oppositeIndices = { 5, 2, 1, 4, 3, 0 };

            var faceIndexData = faceIndex;
            
            _properties.DiceFaces[faceIndexData.FaceIndex].SetFaceNumber(number);
            _properties.DiceFaces[oppositeIndices[faceIndexData.FaceIndex]].SetFaceNumber(7 - number);
            numbers.Remove(number);
            numbers.Remove(7 - number);

            OnCompleteKey = onCompleteKey;

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

            _onRollDone = onRollDone;
            PlayAnimation($"Roll_{faceIndex.AnimName}");
        }
        private void PlayAnimation(string animName)
        {
            var clip = GetAnimationClipByName(animName);
            
            if (clip == null) return;
            
            var originalDuration = clip.length;
            var speedMultiplier = originalDuration / (GameManager.GameState == GameState.Normal ? 2f : .5f);
            _properties.DiceAnimator.speed = speedMultiplier;

            _properties.DiceAnimator.Play(animName, -1, 0);

        }
        private AnimationClip GetAnimationClipByName(string name)
        {
            return _properties.DiceAnimator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == name);
        }

        public void OnRollDoneEvent()
        {
            _onRollDone.Invoke(this);
        }

      
    }
}