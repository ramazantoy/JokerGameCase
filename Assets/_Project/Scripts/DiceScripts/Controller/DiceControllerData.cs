using System.Collections.Generic;
using _Project.Scripts.DiceScripts.Face;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Controller
{
    [System.Serializable]
    public struct DiceControllerData
    {
        public Animator DiceAnimator;
        public DiceDataContainer DiceDataContainer;
        public List<DiceFace> DiceFaces;
    }
}
