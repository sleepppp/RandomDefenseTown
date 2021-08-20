using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    public class World
    {
        Dictionary<TeamType, Team> _teams;

        public void Init()
        {
            if (_teams == null)
                _teams = new Dictionary<TeamType, Team>();

            for(int i =0; i <= (int)TeamType.NoneTeam; ++i)
            {
                Team newTeam = new Team();
                newTeam.Init((TeamType)i);
                _teams.Add((TeamType)i, newTeam);
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


