using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class LoadingScene : MonoBehaviour
    {
      
        private void BlowYourSelf()
        { 
            transform.GetChild(0).gameObject.SetActive(false);
        }

     
    }
}
