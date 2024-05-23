using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.Events.GameEvents
{
    public class OnCollectedItemEvent : IEvent
    {
        public CollectedItemType CollectedItemType;
        public Transform BornTransform;
    }
}
