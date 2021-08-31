using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class Cell
    {
        public Vector3 Position;
        public Vector2 Size;
        public CellState State;
        public CellType Type;

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

        public void Init(Vector3 position, Vector2 size, CellType type, CellState state = CellState.Normal)
        {
            Position = position;
            Size = size;
            Type = type;
            State = state;
        }
    }
}
