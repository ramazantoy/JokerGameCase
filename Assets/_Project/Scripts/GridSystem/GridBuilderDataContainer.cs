using _Project.Scripts.GridSystem.Tile;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.GridSystem
{
    [CreateAssetMenu(fileName = "GridBuilderData", menuName = "ScriptableObjects/GridBuilderDataContainer")]
    public class GridBuilderDataContainer : ScriptableObject
    { 
        [Header("Prefabs")] 
        public TileBase RoadTilePref;
        public TileBase AppleRewardRoadTilePref;
        public TileBase BananaRewardRoadTilePref;
        public TileBase WatermelonRewardRoadTilePref;
        public TileBase EnvTilePref;
        [Header("Spawn Settings")]
        public BuildSettings BuildSettings;
    }

    [System.Serializable]
    public class BuildSettings
    {
        public int RowColCount = 10;
        public Vector3 StartPos = new Vector3(0, 0, -7.5f);
        public float TileSpacing = 1.1f;
        public int RoadStartOffset = 1;
        
        [Range(0,100)]
        public int NoRewardTileRate = 55;
        [Range(0,100)]
        public int BananaRewardTileRate;
        [Range(0,100)]
        public int AppleRewardTileRate;
        [Range(0,100)]
        public int WatermelonRewardTileRate;
    }
    
    [CustomEditor(typeof(GridBuilderDataContainer))]
    public class BuildSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Total rate of all tiles must be %100 for build settings", MessageType.Info);
            DrawDefaultInspector();
        
            
            var container = (GridBuilderDataContainer)target;
            
            var buildSettings = container.BuildSettings;
            
            var totalRate = buildSettings.NoRewardTileRate + buildSettings.BananaRewardTileRate + buildSettings.AppleRewardTileRate + buildSettings.WatermelonRewardTileRate;
            
            if (totalRate != 100)
            {
                EditorGUILayout.HelpBox("Total rate of all tiles must be 100%. Current total: " +"%"+ totalRate , MessageType.Error);
            }
        }
    }
}