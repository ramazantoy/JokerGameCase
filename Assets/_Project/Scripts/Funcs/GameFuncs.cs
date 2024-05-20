using System;
using System.Collections.Generic;
using _Project.Scripts.GridSystem.Tile;
using UnityEngine;

namespace _Project.Scripts.Funcs
{
    public class GameFuncs
    {
        public static Func<int, int, List<RoadTile>> GetMovementTiles;
        public static Func<int, RoadTile> GetRoadTile;
    }
}