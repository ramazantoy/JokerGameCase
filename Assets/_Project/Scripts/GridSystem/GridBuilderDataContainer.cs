using _Project.Scripts.GridSystem.Tile;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

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


        public int TotalRate=>NoRewardTileRate + BananaRewardTileRate +AppleRewardTileRate +WatermelonRewardTileRate;
        public void AutoAdjustRates()
        {
          

            if (TotalRate== 100) return;
            
            if (TotalRate== 0)
            {
                NoRewardTileRate = 25;
                BananaRewardTileRate = 25;
                AppleRewardTileRate = 25;
                WatermelonRewardTileRate = 25;
            }
            else
            {
                var scaleFactor = 100.0f / TotalRate;
                NoRewardTileRate = Mathf.RoundToInt(NoRewardTileRate * scaleFactor);
                BananaRewardTileRate = Mathf.RoundToInt(BananaRewardTileRate * scaleFactor);
                AppleRewardTileRate = Mathf.RoundToInt(AppleRewardTileRate * scaleFactor);
                WatermelonRewardTileRate = Mathf.RoundToInt(WatermelonRewardTileRate * scaleFactor);
            }
        }
        
        public bool IsRatesCorrect(){
            
            return TotalRate==100;
        }

    }
    
 

#if UNITY_EDITOR
    [CustomEditor(typeof(GridBuilderDataContainer))]
    public class BuildSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Total rate of all tiles must be %100 for build settings", MessageType.Info);
            DrawDefaultInspector();
        
            var container = (GridBuilderDataContainer)target;
            
            if (!container.BuildSettings.IsRatesCorrect())
            {
                EditorGUILayout.HelpBox("Total rate of all tiles must be 100%. Current total: %" + container.BuildSettings.TotalRate , MessageType.Error);

                if (GUILayout.Button("Fix Rate"))
                {
                   container.BuildSettings.AutoAdjustRates();
                    EditorUtility.SetDirty(container);
                }
            }
        }
    }
#endif
}
