using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Core;
    public class Team
    {
        TeamType _teamType;

        Architecture _architecture;

        Dictionary<Muid, WorldObject> _units;
        Dictionary<Muid, WorldObject> _towers;

        public TeamType TeamType { get { return _teamType; } }
        public Architecture Architecture { get { return _architecture; } }

        public void Init(TeamType teamType)
        {
            _teamType = teamType;

            if (_units == null)
                _units = new Dictionary<Muid, WorldObject>();
            if (_towers == null)
                _towers = new Dictionary<Muid, WorldObject>();

            WorldObject[] allWorldObjects = MonoBehaviour.FindObjectsOfType<WorldObject>();

            for (int i = 0; i < allWorldObjects.Length; ++i)
            {
                if (allWorldObjects[i].TeamType != _teamType)
                    continue;

                allWorldObjects[i].StaticInit();

                if (allWorldObjects[i].WorldObjectType == WorldObjectType.Architecture)
                {
                    _architecture = allWorldObjects[i] as Architecture;
                }
                else if (allWorldObjects[i].WorldObjectType == WorldObjectType.Tower)
                {
                    AddTower(allWorldObjects[i]);
                }
                else if (allWorldObjects[i].WorldObjectType == WorldObjectType.Unit)
                {
                    AddTower(allWorldObjects[i]);
                }
            }
        }

        public void AddTower(WorldObject obj)
        {
            if (obj is Tower == false)
                return;
            _towers.Add(obj.Muid, obj);
        }

        public void AddUnit(WorldObject obj)
        {
            if (obj is Unit == false)
                return;
            _units.Add(obj.Muid, obj);
        }

        public WorldObject GetWorldObject(Muid muid)
        {
            WorldObject result = null;

            result = _units.TryGetValue(muid);
            if (result != null)
                return result;
            result = _towers.TryGetValue(muid);

            return result;
        }

        public WorldObject GetUnit(Muid muid)
        {
            return _units.TryGetValue(muid);
        }

        public WorldObject GetTower(Muid muid)
        {
            return _towers.TryGetValue(muid);
        }

        public List<WorldObject> GetPlayerUnits()
        {
            List<WorldObject> result = new List<WorldObject>();
            foreach (WorldObject obj in _units.Values)
            {
                if (obj.IsPlayerOwner)
                    result.Add(obj);
            }
            return result;
        }

        public List<WorldObject> GetPlayerTower()
        {
            List<WorldObject> result = new List<WorldObject>();
            foreach (WorldObject obj in _towers.Values)
            {
                if (obj.IsPlayerOwner)
                    result.Add(obj);
            }
            return result;
        }
    }
}


