using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public enum TeamType : int
    {
        Error = -1,
        UserTeam = 0,
        EnemyTeam = 1,
        NoneTeam = 2,
    }

    public enum WorldObjectType : int
    {
        None = -1,
        Architecture = 0,
        Unit = 1,
        Tower = 2,
    }

    public enum CellType : int
    {
        Moveable = 0,
        Block = 1,
    }

    public enum CellState : int
    {
        Normal = 0,
        Block = 1,
    }

}


