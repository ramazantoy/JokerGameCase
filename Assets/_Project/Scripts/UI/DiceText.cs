using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class DiceText : MonoBehaviour,ISelectHandler
    {
        [SerializeField]
        private TMP_InputField _inputField;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(delegate { OnInputValueChanged(); });
        }

        private void OnInputValueChanged()
        {
            EventBus<OnPlayClickSoundEvent>.Publish(new OnPlayClickSoundEvent());
            if (!int.TryParse(_inputField.text, out var value) || value is < 1 or > 6)
            {
                _inputField.text = "";
            }
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            _inputField.text = "";
       
        }
        

    }
}
