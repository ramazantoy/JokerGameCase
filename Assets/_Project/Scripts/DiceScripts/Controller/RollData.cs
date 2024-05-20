
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.DiceScripts.Controller
{
    public struct RollData
    {
        public int RollValue;
        public List<Dice> RolledDices;

        public RollData(int rollValue, List<Dice> rolledDices)
        {
            RollValue = rollValue;
            RolledDices = rolledDices;
        }
    }
}
