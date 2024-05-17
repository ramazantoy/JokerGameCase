using System;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        
        private void Update()
        {
            ActionHandler.UpdateEvent?.Invoke(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            ActionHandler.FixedUpdateEvent?.Invoke(Time.fixedDeltaTime);
        }
    }
}