using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public enum TeamType : int
    {
        Error = -1,
        PlayerTeam = 0,
        FriendlyTeam = 1,
        EnemyTeam = 2,
        NoneTeam = 3,
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

    public enum TowerType : int
    {
        UnitCreation = 0,
    }
}


