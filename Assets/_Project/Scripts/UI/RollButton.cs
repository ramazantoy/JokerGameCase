using System;
using _Project.Scripts.DiceScripts;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RollButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _textMeshes;

        [SerializeField]
        private DiceController _diceController;
        public void OnButtonDown()
        {
            if (_textMeshes.Length >= 2)
            {
                char[] textChars1 = _textMeshes[0].text.ToCharArray();
                char[] textChars2 = _textMeshes[1].text.ToCharArray();

                if (textChars1.Length > 0 && textChars2.Length > 0 &&
                    char.IsDigit(textChars1[0]) && char.IsDigit(textChars2[0]))
                {
                    int number1 = int.Parse(textChars1[0].ToString());
                    int number2 = int.Parse(textChars2[0].ToString());

               
                }
                else
                {
                    Debug.LogError("Dize içeriği tamsayıya dönüştürülemedi veya boş.");
                }
            }
            else
            {
                Debug.LogError("TextMesh dizisi yeterince uzun değil!");
            }
        
        }
    }
}
