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
            int value;
            
            if (!int.TryParse(_inputField.text, out value) || value < 1 || value > 6)
            {
                _inputField.text = "1";
            }
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            _inputField.text = "";
        }

    }
}
