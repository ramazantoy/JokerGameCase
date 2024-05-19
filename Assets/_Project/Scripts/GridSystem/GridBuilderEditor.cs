using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.GridSystem
{
    [CustomEditor(typeof(GridBuilder),true)]
    public class GridBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Build"))
            {
                var gridBuilder = (GridBuilder)target;
                gridBuilder.BuildMapOnEditor();
            }
            
            if (GUILayout.Button("RemoveTiles"))
            {
                var gridBuilder = (GridBuilder)target;
                gridBuilder.RemoveTiles();
            }
        }
    }
}
