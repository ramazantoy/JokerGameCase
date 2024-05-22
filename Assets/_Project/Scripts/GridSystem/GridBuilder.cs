using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Events.EventBusScripts;
using _Project.Scripts.Events.GameEvents;
using _Project.Scripts.Funcs;
using _Project.Scripts.GridSystem.Tile;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Project.Scripts.GridSystem
{
    public class GridBuilder : MonoBehaviour
    {
         [SerializeField] private GridBuilderDataContainer _gridBuilderDataContainer;

        [SerializeField] private Transform _envTilesParent;
        [SerializeField] private Transform _roadTilesParent;

        private TileBase[,] tiles;

        [SerializeField]
        private List<RoadTileBase> _roadTiles;

        private void OnEnable()
        {
            GameFuncs.GetRoadTile += GetRoadTile;
        }

        private void OnDisable()
        {
            GameFuncs.GetRoadTile -= GetRoadTile;
        }

        private void Start()
        {
            if (_roadTiles == null || _roadTiles.Count <= 0)
            {
                BuildMapOnEditor(false);
            }

            for (var i = 0; i < _roadTiles.Count; i++)
            {
                _roadTiles[i].TileIndex = i;
            }

            StartAnim().Forget();
        }

        private async UniTaskVoid StartAnim()
        {
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());

            foreach (var tileBase in childList)
            {
                tileBase.transform.localScale = Vector3.zero;
            }

            foreach (var tileBase in childList)
            {
                tileBase.StartAnim(.1f);
                await UniTask.WaitForSeconds(.025f);
            }

            EventBus<OnBoardReadyEvent>.Publish(new OnBoardReadyEvent());
        }
        
        public void BuildMapOnEditor(bool useInEditor = true)
        {
            _roadTiles.Clear();
            
            if (useInEditor)
            {
                Debug.LogWarning("Map Building On Editor");
            }
      

            var rowRolCount = _gridBuilderDataContainer.BuildSettings.RowColCount;
            tiles = new TileBase[rowRolCount, rowRolCount];

            var startPos = _gridBuilderDataContainer.BuildSettings.StartPos;
            var tileSpacing = _gridBuilderDataContainer.BuildSettings.TileSpacing;
            var startIndex = _gridBuilderDataContainer.BuildSettings.RoadStartOffset;

            for (int i = 0; i < rowRolCount; i++)
            {
                for (int j = 0; j < rowRolCount; j++)
                {
                    var tile = useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.EnvTilePref, _envTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.EnvTilePref, _envTilesParent.transform);
                    tile.gameObject.name = $"EnvTile {i}_{j}";
                    tile.transform.position = startPos + new Vector3(i * tileSpacing, 0, j * tileSpacing);
                    tiles[i, j] = tile;
                }
            }

            var tileIndex = 1;

            SetRoadTile(startIndex, startIndex, ref tileIndex, "#", useInEditor); // First Road

            for (int j = startIndex + 1; j < rowRolCount - startIndex; j++)
            {
                if (startIndex== 1 && j == 1)
                {
                    continue;
                }
                SetRoadTile(startIndex, j, ref tileIndex, "", useInEditor);
            }

            for (int i = startIndex + 1; i < rowRolCount - startIndex; i++)
            {
                SetRoadTile(i, rowRolCount - 1 - startIndex, ref tileIndex, "", useInEditor);
            }

            for (int j = rowRolCount - 2 - startIndex; j >= startIndex; j--)
            {
                SetRoadTile(rowRolCount - 1 - startIndex, j, ref tileIndex, "", useInEditor);
            }

            for (int i = rowRolCount - 2 - startIndex; i > startIndex; i--)
            {
                SetRoadTile(i, startIndex, ref tileIndex, "", useInEditor);
            }
        }

        private void SetRoadTile(int i, int j, ref int tileIndex, string text = "", bool useInEditor = true)
        {
            if (tiles[i, j] != null)
            {
                DestroyImmediate(tiles[i, j].gameObject);
            }

            var tile = InstantiateRewardTile(i, j, useInEditor);

            if (tile == null) return;
            
            tile.gameObject.name = $"RoadTile {i}_{j}";
            tile.transform.position = _gridBuilderDataContainer.BuildSettings.StartPos + new Vector3(i * _gridBuilderDataContainer.BuildSettings.TileSpacing, 0f, j * _gridBuilderDataContainer.BuildSettings.TileSpacing);
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

            var roadTile = tile as RoadTileBase;
            _roadTiles.Add(roadTile);
        }

        private TileBase InstantiateRewardTile(int i, int j, bool useInEditor)
        {
            if (i == 1 && j == 1) // first tile
            {
                return useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.RoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.RoadTilePref, _roadTilesParent.transform);
            }

            var randomValue = Random.Range(0, 100);
            var cumulativeRate = 0;

            cumulativeRate += _gridBuilderDataContainer.BuildSettings.NoRewardTileRate;
            if (randomValue < cumulativeRate)
            {
                return useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.RoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.RoadTilePref, _roadTilesParent.transform);
            }

            cumulativeRate += _gridBuilderDataContainer.BuildSettings.BananaRewardTileRate;
            if (randomValue < cumulativeRate)
            {
                return useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.BananaRewardRoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.BananaRewardRoadTilePref, _roadTilesParent.transform);
            }

            cumulativeRate += _gridBuilderDataContainer.BuildSettings.AppleRewardTileRate;
            if (randomValue < cumulativeRate)
            {
                return useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.AppleRewardRoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.AppleRewardRoadTilePref, _roadTilesParent.transform);
            }

            cumulativeRate += _gridBuilderDataContainer.BuildSettings.WatermelonRewardTileRate;
            if (randomValue < cumulativeRate)
            {
                return useInEditor ? PrefabUtility.InstantiatePrefab(_gridBuilderDataContainer.WatermelonRewardRoadTilePref, _roadTilesParent.transform) as TileBase : Instantiate(_gridBuilderDataContainer.WatermelonRewardRoadTilePref, _roadTilesParent.transform);
            }

            return null;
        }

#if UNITY_EDITOR
        public void RemoveTiles()
        {
            Debug.LogWarning("Remove Tiles On Editor");
            var childList = new List<TileBase>(transform.GetComponentsInChildren<TileBase>());
            _roadTiles.Clear();

            foreach (var child in childList)
            {
                DestroyImmediate(child.gameObject);
            }
        }
#endif
    

        private RoadTileBase GetRoadTile(int index)
        {
            return _roadTiles[index % _roadTiles.Count];
        }
    }
}