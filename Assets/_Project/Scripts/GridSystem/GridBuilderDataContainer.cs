using _Project.Scripts.GridSystem.Tile;
using UnityEngine;

namespace _Project.Scripts.GridSystem
{
    [CreateAssetMenu(fileName = "GridBuilderData", menuName = "ScriptableObjects/GridBuilderDataContainer")]
    public class GridBuilderDataContainer : ScriptableObject
    {
        [Header("Prefabs")] 
        public TileBase RoadTilePref;
        public TileBase EnvTilePref;
        [Header("Spawn Settings")] 
        public int RowColCount = 5;
        public Vector3 StartPos = new Vector3(0, 0, -7.5f);
        public float TileSpacing = 1.1f;
        public int RoadStartOffset = 3;
    }
}