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

        private Transform[,] tiles;


        public void BuildMapOnEditor()
        {
            Debug.LogWarning("Map Building On Editor");

            var rowRolCount = _gridBuilderDataContainer.RowColCount;
            tiles = new Transform[rowRolCount, rowRolCount];

            var envTilePref = _gridBuilderDataContainer.EnvTilePref;

            var startPos = _gridBuilderDataContainer.StartPos;

            var tileSpacing = _gridBuilderDataContainer.TileSpacing;

            var startIndex = _gridBuilderDataContainer.RoadStartOffset;

            var roadPrefab = _gridBuilderDataContainer.RoadTilePref;


            for (int i = 0; i < rowRolCount; i++)
            {
                for (int j = 0; j < rowRolCount; j++)
                {
                    var tile = PrefabUtility.InstantiatePrefab(envTilePref, _envTilesParent.transform) as TileBase;
                    tile.gameObject.name = $"EnvTile {i}_{j}";
                    tile.transform.position = startPos + new Vector3(i * tileSpacing, 0, j * tileSpacing);
                    tiles[i, j] = tile.transform;
                }
            }

            for (int i = startIndex; i < rowRolCount - startIndex; i++)
            {
                for (int j = startIndex; j < rowRolCount - startIndex; j++)
                {
                    if (i == startIndex || i == rowRolCount - 1 - startIndex || j == startIndex ||
                        j == rowRolCount - 1 - startIndex)
                    {
                        if (tiles[i, j] != null)
                        {
                            DestroyImmediate(tiles[i, j].gameObject);
                        }

                        var tile = PrefabUtility.InstantiatePrefab(roadPrefab, _roadTilesParent.transform) as TileBase;
                        tile.gameObject.name = $"RoadTile {i}_{j}";
                        tile.transform.position = startPos + new Vector3(i * tileSpacing, 0f, j * tileSpacing);
                        tiles[i, j] = tile.transform;
                    }
                }
            }
        }

        // private IEnumerator BuildTiles()
        // {
        //     tiles = new Transform[_rowColCount, _rowColCount];
        //     for (int i = 0; i < _rowColCount; i++)
        //     {
        //         for (int j = 0; j < _rowColCount; j++)
        //         {
        //             var tile = Instantiate(_envPrefab, transform);
        //
        //             tile.gameObject.name = $"EnvTile_{i}_{j}";
        //             tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0, j * _tileSpacing);
        //             tile.transform.localScale = Vector3.zero;
        //             tile.transform.DOScale(new Vector3(1, .1f, 1f), .1f);
        //             tiles[i, j] = tile.transform;
        //             yield return new WaitForSeconds(.05f);
        //         }
        //     }
        //
        //     for (int i = startIndex; i < _rowColCount - startIndex; i++)
        //     {
        //         for (int j = startIndex; j < _rowColCount - startIndex; j++)
        //         {
        //             if (i == startIndex || i == _rowColCount - 1 - startIndex || j == startIndex ||
        //                 j == _rowColCount - 1 - startIndex)
        //             {
        //                 if (tiles[i, j] != null)
        //                 {
        //                     DestroyImmediate(tiles[i, j].gameObject);
        //                 }
        //
        //                 var tile = Instantiate(_roadPrefab, transform);
        //                 tile.gameObject.name = $"RoadTile_{i}_{j}";
        //                 tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0f, j * _tileSpacing);
        //                 tiles[i, j] = tile.transform;
        //
        //                 tile.transform.localScale = Vector3.zero;
        //                 tile.transform.DOScale(new Vector3(1, .1f, 1f), .1f);
        //
        //                 yield return new WaitForSeconds(.05f);
        //             }
        //         }
        //     }
        // }


        public void RemoveTiles()
        {
            Debug.LogWarning("Remove Tiles  On Editor");
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());

            foreach (var child in childList)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}