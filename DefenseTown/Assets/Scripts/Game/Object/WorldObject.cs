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

        //Scene 시작할 때 호출
        public virtual void StaticInit()
        {
            Muid = new Muid(WorldObjectType);
            _team = Game.Instance.World.GetTeam(TeamType);
        }

        //동적생성때 호출
        public virtual void DynamicInit(WorldObjectType objectType, TeamType teamType, bool isPlayerOwner)
        {
            Muid = new Muid(WorldObjectType);
            WorldObjectType = objectType;
            TeamType = teamType;
            IsPlayerOwner = isPlayerOwner;
            _team = Game.Instance.World.GetTeam(TeamType);
        }

        //피킹 했을 때
        public virtual void OnPickStart()
        {

        }

        //피킹 해제 했을 때
        public virtual void OnPickEnd()
        {

        }

        //Pick되어 있는 동안 Update호출
        public virtual void OnPickUpdate()
        {

        }
    }
}


