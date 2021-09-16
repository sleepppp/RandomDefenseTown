using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    public class WorldObject : MonoBehaviour
    {
        [Header("WorldObject")]
        public WorldObjectType WorldObjectType;
        public TeamType TeamType;
        public bool IsPlayerOwner;
        [HideInInspector] public Muid Muid;

        //Scene 시작할 때 호출
        public virtual void StaticInit()
        {
            Muid = new Muid(WorldObjectType);
        }

        //동적생성때 호출
        public virtual void DynamicInit(WorldObjectType objectType, TeamType teamType, bool isPlayerOwner)
        {
            WorldObjectType = objectType;
            Muid = new Muid(WorldObjectType);
            TeamType = teamType;
            IsPlayerOwner = isPlayerOwner;
        }

        public Team GetTeam()
        {
            return Game.Instance.World.GetTeam(TeamType);
        }

        //피킹 했을 때
        public virtual void OnPickStart()
        {

        }

        //피킹 해제 했을 때
        public virtual void OnPickEnd()
        {

        }

        //Pick되어 있는 동안 호출
        public virtual void OnPickUpdate()
        {

        }
    }
}


