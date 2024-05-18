using _Project.Scripts.Events;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RollButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _textMeshes;
        
        public void OnButtonDown()
        {
            if (_textMeshes.Length >= 2)
            {
                var textChars1 = _textMeshes[0].text.ToCharArray();
                var textChars2 = _textMeshes[1].text.ToCharArray();

                if (textChars1.Length <= 0 || textChars2.Length <= 0 || !char.IsDigit(textChars1[0]) || !char.IsDigit(textChars2[0])) return;
                
                
                var number1 = int.Parse(textChars1[0].ToString());
                var number2 = int.Parse(textChars2[0].ToString());

                EventBus.OnRollDices?.Invoke(number1,number2);
            }
            
          
           
        
        }
    }
}
