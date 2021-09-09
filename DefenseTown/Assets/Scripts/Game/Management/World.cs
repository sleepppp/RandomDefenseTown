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

        WorldObjectPickSystem _pickSystem;

        public void Init()
        {
            if (Grid == null)
                Grid = FindObjectOfType<Grid>();

            if (_teams == null)
                _teams = new Dictionary<TeamType, Team>();

            if (_pickSystem == null)
                _pickSystem = new WorldObjectPickSystem();

            //todo 데이터 생기면 추 후 수정
            for (int i =0; i <= (int)TeamType.NoneTeam; ++i)
            {
                Team newTeam = new Team();
                newTeam.Init((TeamType)i);
                _teams.Add((TeamType)i, newTeam);
            }
        }

        public void GameUpdate()
        {
            _pickSystem.GameUpdate();
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


