
using System.Collections.Generic;
using _Project.Scripts.LeonsExtensions;
using UnityEngine;

namespace _Project.Scripts
{
    public class RandomSkyColorHandler : MonoBehaviour
    {
        [SerializeField] 
        private Material _skyboxMaterial;
        [SerializeField]
        private List<Color> _bgColors;
        void Start()
        {
          _skyboxMaterial.SetColor("_SkyGradientBottom",_bgColors.GetRandom());
        }

      
    }
}
