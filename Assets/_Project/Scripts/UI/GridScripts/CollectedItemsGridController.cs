using System.Collections.Generic;
using _Project.Scripts.Funcs;
using UnityEngine;

namespace _Project.Scripts.UI.GridScripts
{
    public class CollectedItemsGridController : GridControllerUI
    {
        [SerializeField]
        private List<CollectedItemGroup> _collectedItemGroups;

        private CollectedItemGroup GetCollectedItemGroup(int index)
        {
            return _collectedItemGroups[index];
        }

        public override void OnEnable()
        {
            base.OnEnable();

            GameFuncs.GetCollectedItemGroup += GetCollectedItemGroup;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            GameFuncs.GetCollectedItemGroup -= GetCollectedItemGroup;
        }
    }
}
