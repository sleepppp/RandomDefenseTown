using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    //todo 길찾기 작업 필요, 우선 인터페이스만 정의
    public class PathFinder
    {
        public class CellData
        {
            public Cell Cell;
            public bool isOpen;
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

        List<Cell> StartPathFinding(Vector2Int start, Vector2Int destination)
        {
            List<Cell> result = new List<Cell>();

            return result;
        }
    }
}
