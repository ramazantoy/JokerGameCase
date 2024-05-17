using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.GridSystem
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject _roadPrefab;
        [SerializeField] private GameObject _envPrefab;
        [SerializeField] private int _rowColCount = 5;
        [SerializeField] private Vector3 _startPos = new Vector3(0, 0, -8f);
        [SerializeField] private float _tileSpacing = 1.1f;

        [SerializeField]
        private int startIndex = 3;
        
        private Transform[,] tiles;


        private void Start()
        {
            StartCoroutine(BuildTiles());
        }

        public void BuildSquare()
        {
         tiles = new Transform[_rowColCount, _rowColCount];
            
            for (int i = 0; i < _rowColCount; i++)
            {
                for (int j = 0; j < _rowColCount; j++)
                {
                    var tile = Instantiate(_envPrefab, transform);
                    tile.gameObject.name = $"EnvTile_{i}_{j}";
                    tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0, j * _tileSpacing);
                    tiles[i, j] = tile.transform;
                }
            }
            for (int i = startIndex; i < _rowColCount - startIndex; i++)
            {
                for (int j = startIndex; j < _rowColCount - startIndex; j++)
                {
                    if (i == startIndex || i == _rowColCount - 1 - startIndex || j == startIndex || j == _rowColCount - 1 - startIndex)
                    {
                        if (tiles[i, j] != null)
                        {
                            DestroyImmediate(tiles[i,j].gameObject);
                        }
                        var tile = Instantiate(_roadPrefab, transform);
                        tile.gameObject.name = $"RoadTile_{i}_{j}";
                        tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0f, j * _tileSpacing);
                        tiles[i, j] = tile.transform;
                    }
                }
            }
        }

        private IEnumerator BuildTiles()
        {
            tiles = new Transform[_rowColCount, _rowColCount];  
            for (int i = 0; i < _rowColCount; i++)
            {
                for (int j = 0; j < _rowColCount; j++)
                {
                    var tile = Instantiate(_envPrefab, transform);
                   
                    tile.gameObject.name = $"EnvTile_{i}_{j}";
                    tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0, j * _tileSpacing);
                    tile.transform.localScale = Vector3.zero;
                    tile.transform.DOScale(new Vector3(1,.1f,1f), .1f);
                    tiles[i, j] = tile.transform;
                    yield return new WaitForSeconds(.05f);
                }
            }
            for (int i = startIndex; i < _rowColCount - startIndex; i++)
            {
                for (int j = startIndex; j < _rowColCount - startIndex; j++)
                {
                    if (i == startIndex || i == _rowColCount - 1 - startIndex || j == startIndex || j == _rowColCount - 1 - startIndex)
                    {
                        if (tiles[i, j] != null)
                        {
                            DestroyImmediate(tiles[i,j].gameObject);
                        }
                        var tile = Instantiate(_roadPrefab, transform);
                        tile.gameObject.name = $"RoadTile_{i}_{j}";
                        tile.transform.position = _startPos + new Vector3(i * _tileSpacing, 0f, j * _tileSpacing);
                        tiles[i, j] = tile.transform;
                        
                        tile.transform.localScale = Vector3.zero;
                        tile.transform.DOScale(new Vector3(1,.1f,1f), .1f);

                        yield return new WaitForSeconds(.05f);
                    }
                }
            }
        }

    
    public void RemoveTiles()
        {
            var childList = new List<Transform>(transform.GetComponentsInChildren<Transform>());
            childList.Remove(transform); 
    
            foreach (var child in childList)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
