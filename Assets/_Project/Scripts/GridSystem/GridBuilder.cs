using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.GridSystem.Tile;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.GridSystem
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] private GridBuilderDataContainer _gridBuilderDataContainer;

        [SerializeField] private Transform _envTilesParent;
        [SerializeField] private Transform _roadTilesParent;
        

        
        private TileBase[,] tiles;

        [SerializeField]
        private List<RoadTile> _roadTiles;

        private void Start()
        {
            if (_roadTiles==null || _roadTiles.Count<=0)
            { 
                BuildMapOnEditor(false);
            }

            StartCoroutine(StartAnim());
        }

        private IEnumerator StartAnim()
        {
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());
            
            foreach (var tileBase in childList)
            {
                tileBase.transform.localScale=Vector3.zero;
            }
            
            foreach (var tileBase in childList)
            {
                tileBase.StartAnim(.1f);
                yield return new WaitForSeconds(.025f);
            }
            
        }

#if UNITY_EDITOR

        public void BuildMapOnEditor(bool useInEditor=true)
        {
            _roadTiles.Clear();
            Debug.LogWarning("Map Building On Editor");

            var rowRolCount = _gridBuilderDataContainer.RowColCount;
            tiles = new TileBase[rowRolCount, rowRolCount];

           

            var startPos = _gridBuilderDataContainer.StartPos;

            var tileSpacing = _gridBuilderDataContainer.TileSpacing;

            var startIndex = _gridBuilderDataContainer.RoadStartOffset;
            

            for (int i = 0; i < rowRolCount; i++)
            {
                for (int j = 0; j < rowRolCount; j++)
                {
                    var tile = useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.EnvTilePref, _envTilesParent.transform) as TileBase  : Instantiate(_gridBuilderDataContainer.EnvTilePref, _envTilesParent.transform);
                    tile.gameObject.name = $"EnvTile {i}_{j}";
                    tile.transform.position = startPos + new Vector3(i * tileSpacing, 0, j * tileSpacing);
                    tiles[i, j] = tile;
                }
            }

            var tileIndex = 1;
            
            SetRoadTile(startIndex, startIndex, ref tileIndex, "#");//First Road 

            for (int j = startIndex + 1; j < rowRolCount - startIndex; j++)
            {
                SetRoadTile(startIndex, j, ref tileIndex);
            }

            for (int i = startIndex + 1; i < rowRolCount - startIndex; i++)
            {
                SetRoadTile(i, rowRolCount - 1 - startIndex, ref tileIndex);
            }

            for (int j = rowRolCount - 2 - startIndex; j >= startIndex; j--)
            {
                SetRoadTile(rowRolCount - 1 - startIndex, j, ref tileIndex);
            }

            for (int i = rowRolCount - 2 - startIndex; i > startIndex; i--)
            {
                SetRoadTile(i, startIndex, ref tileIndex);
            }
        }

        private void SetRoadTile(int i, int j, ref int tileIndex, string text = "",bool useInEditor=true)
        {
            if (tiles[i, j] != null)
            {
                DestroyImmediate(tiles[i, j].gameObject);
            }

            var tile =  useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.RoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.RoadTilePref, _envTilesParent.transform);
            tile.gameObject.name = $"RoadTile {i}_{j}";
            tile.transform.position = _gridBuilderDataContainer.StartPos + new Vector3(i * _gridBuilderDataContainer.TileSpacing, 0f, j * _gridBuilderDataContainer.TileSpacing);
            tiles[i, j] = tile;

            if (!string.IsNullOrEmpty(text))
            {
                tile.SetText(text);
            }
            else
            {
                tile.SetText(tileIndex.ToString());
                tileIndex++;
            }
            _roadTiles.Add(tile as RoadTile);
        }


        public void RemoveTiles()
        {
            Debug.LogWarning("Remove Tiles  On Editor");
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());

            foreach (var child in childList)
            {
                DestroyImmediate(child.gameObject);
            }
        }
#endif

        // public List<TileBase> GetNeighbors(Vector2Int coordinate)
        // {
        //     var neighbors = new List<TileBase>();
        //     int row = coordinate.x;
        //     int col = coordinate.y;
        //     int[] dRow = { -1, 1, 0, 0, -1, -1, 1, 1 };
        //     int[] dCol = { 0, 0, -1, 1, -1, 1, -1, 1 };
        //
        //     for (int i = 0; i < 8; i++)
        //     {
        //         int newRow = row + dRow[i];
        //         int newCol = col + dCol[i];
        //
        //         if (newRow >= 0 && newRow < tiles.GetLength(0) && newCol >= 0 && newCol < tiles.GetLength(1) &&
        //             tiles[newRow, newCol] != null)
        //         {
        //             neighbors.Add(tiles[newRow, newCol]);
        //         }
        //     }
        //
        //     return neighbors;
        // }
    }
}