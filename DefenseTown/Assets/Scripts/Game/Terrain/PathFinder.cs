using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    //todo 길찾기 작업 필요, 우선 인터페이스만 정의
    public class PathFinder
    {
        public struct CellData
        {
            public Cell Cell;
            public bool IsOpen;
            public bool IsClose;
            public int CostTotal;
            public int CostFromStart;
            public int CostToEnd;

            public void CalcCost()
            {
                CostTotal = CostFromStart + CostToEnd;
            }
        }

        Grid _grid;

        public void Init(Grid grid)
        {
            _grid = grid;
        }

        public List<Cell> GetPath(Vector3 start, Vector3 destination)
        {
            Vector2Int startIndex = Grid.TransformPositionToIndex(start);
            Vector2Int destinationIndex = Grid.TransformPositionToIndex(destination);
            return GetPath(startIndex, destinationIndex);
        }

        public List<Cell> GetPath(Vector2Int start,Vector2Int destination)
        {
            if (Game.Instance.World.Grid.IsOutOfRange(start.x, start.y))
                return null;
            if (Game.Instance.World.Grid.IsOutOfRange(destination.x, destination.y))
                return null;

            return StartPathFinding(start,destination);
        }

        int CalcHeuristic(Vector2Int indexA, Vector2Int indexB,int tileSize = 1)
        {
            int dx = Mathf.Abs(indexA.x - indexB.x);
            int dy = Mathf.Abs(indexA.y - indexB.y);
            int diagonal = Mathf.Min(dx, dy);
            return (int)((dx - diagonal + dy - diagonal) * tileSize + diagonal * tileSize * Mathf.Sqrt(2));
        }

        List<Cell> StartPathFinding(Vector2Int start, Vector2Int destination)
        {
            List<Cell> result = new List<Cell>();

            if (start == destination) return result;
            if (_grid.IsOutOfRange(start.x, start.y)) return result;
            if (_grid.IsOutOfRange(destination.x, destination.y)) return result;

            CellData[,] dummyList = new CellData[_grid.CellHeightCount, _grid.CellWidthCount];

            return result;
        }
    }
}
