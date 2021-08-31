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

        //Scene ������ �� ȣ��
        public virtual void StaticInit()
        {
            Muid = new Muid(WorldObjectType);
        }

        //���������� ȣ��
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

        //��ŷ ���� ��
        public virtual void OnPickStart()
        {

        }

        //��ŷ ���� ���� ��
        public virtual void OnPickEnd()
        {

        }

        //Pick�Ǿ� �ִ� ���� ȣ��
        public virtual void OnPickUpdate()
        {

        }
    }
}


