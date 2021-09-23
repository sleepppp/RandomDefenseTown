using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class Cell
    {
        public Vector3 Position;
        public Vector2 Size;
        public Vector2Int Index;
        public CellState State;
        public CellType Type;
        public List<WorldObject> OnObjects;

        public Vector3 CenterPos
        {
            get
            {
                return Position + new Vector3(Size.x, 0f, Size.y) * 0.5f;
            }
        }

        public bool CanPass
        {
            get
            {
                return Type != CellType.Block && State != CellState.Block;
            }
        }

        public void Init(Vector3 position,Vector2Int index, Vector2 size, CellType type, CellState state = CellState.Normal)
        {
            Position = position;
            Index = index;
            Size = size;
            Type = type;
            State = state;
            OnObjects = new List<WorldObject>();
        }

        public void Connect(WorldObject onObject)
        {
            OnObjects.AddUnique(onObject);

            State = CellState.Block;
        }

        public void DisConnect(WorldObject onObject)
        {
            if(OnObjects.Contains(onObject))
                OnObjects.Remove(onObject);

            State = OnObjects.Count == 0 ? CellState.Normal : CellState.Block;
        }

        public bool CanCreateTower()
        {
            return CanPass;
        }

        public TowerBase GetTower()
        {
            foreach(WorldObject worldObject in OnObjects)
            {
                if (worldObject.IsTower())
                    return worldObject as TowerBase;
            }

            return null;
        }
    }
}
