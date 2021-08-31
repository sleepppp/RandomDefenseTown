using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    public class World : MonoBehaviour
    {
        public Grid Grid;

        Dictionary<TeamType, Team> _teams;

        List<WorldObject> _pickList;    //만약 군집효과 구현이 필요하면 이렇게 처리하지말고 별도의 객체로 빼서 처리

        public void Init()
        {
            if (Grid == null)
                Grid = FindObjectOfType<Grid>();
            //todo Grid Init from data

            if (_pickList == null)
                _pickList = new List<WorldObject>();

            if (_teams == null)
                _teams = new Dictionary<TeamType, Team>();

            for(int i =0; i <= (int)TeamType.NoneTeam; ++i)
            {
                Team newTeam = new Team();
                newTeam.Init((TeamType)i);
                _teams.Add((TeamType)i, newTeam);
            }
        }

        public void GameUpdate()
        {
            for(int i =0; i < _pickList.Count; ++i)
            {
                _pickList[i].OnPickUpdate();
            }
        }

        public Team GetTeam(TeamType type)
        {
            return _teams.TryGetValue(type);
        }

        public WorldObject GetUnit(Muid muid)
        {
            WorldObject result = null;
            foreach(Team team in _teams.Values)
            {
                result = team.GetUnit(muid);
                if (result != null)
                    return result;
            }
            return result;
        }

        public WorldObject GetTower(Muid muid)
        {
            WorldObject result = null;
            foreach (Team team in _teams.Values)
            {
                result = team.GetTower(muid);
                if (result != null)
                    return result;
            }
            return result;
        }
    }
}


