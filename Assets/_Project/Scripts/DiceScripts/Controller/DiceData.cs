using System.Collections.Generic;
using _Project.Scripts.DiceScripts.Face;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Controller
{
    [System.Serializable]
    public struct DiceData
    {
        public Animator DiceAnimator;
        public GameObject DiceParticle;
        public List<DiceFace> DiceFaces;
    }
}
