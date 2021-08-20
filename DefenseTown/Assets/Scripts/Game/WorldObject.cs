using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    public class WorldObject : MonoBehaviour
    {
        [Header("WorldObject")]
        public WorldObjectType WorldObjectType;
        public TeamType TeamType;
        public bool IsPlayerOwner;
        [System.NonSerialized] public Muid Muid;
        Team _team;

        public virtual void StaticInit()
        {
            Muid = new Muid(WorldObjectType);
            _team = Game.Instance.World.GetTeam(TeamType);
        }

        public virtual void DynamicInit(WorldObjectType objectType, TeamType teamType, bool isPlayerOwner)
        {
            Muid = new Muid(WorldObjectType);
            WorldObjectType = objectType;
            TeamType = teamType;
            IsPlayerOwner = isPlayerOwner;
            _team = Game.Instance.World.GetTeam(TeamType);
        }
    }
}


