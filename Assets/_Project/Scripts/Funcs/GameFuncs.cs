using System;
using System.Collections.Generic;
using _Project.Scripts.GridSystem.Tile;
using _Project.Scripts.UI.GridScripts;
using UnityEngine;

namespace _Project.Scripts.Funcs
{
    public class GameFuncs
    {
        public static Func<int, CollectedItemGroup> GetCollectedItemGroup;
        public static Func<int, RoadTileBase> GetRoadTile;
    }
}