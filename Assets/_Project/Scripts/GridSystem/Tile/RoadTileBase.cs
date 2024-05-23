using _Project.Scripts.SaveSystem;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GridSystem.Tile
{
    public  abstract class RoadTileBase : TileBase
    {
        [SerializeField]
        private ParticleSystem _tileParticle;
        public int TileIndex { get; set; }
        
        public TextMeshPro TileText;
        public override void SetText(string text)
        {
            TileText.text = text;
        }

        public void PlayParticle()
        {
            _tileParticle.Play();
        }

        public abstract (CollectedItemType, int) GiveRewards();

    }
}
