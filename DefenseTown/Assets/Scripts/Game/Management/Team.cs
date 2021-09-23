using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    public class Team
    {
        public string Name { get; private set; }
        public int CurrentHP { get; private set; }
        public int FullHP { get; private set; }
        public int CurrentMP { get; private set; }
        public int FullMP { get; private set; }
        public int CurrentMoney { get; private set; }
        public int MaxUnitCount { get; private set; }
        public int MaxTowerCount { get; private set; }

        TeamType _teamType;

        Architecture _architecture;

        Dictionary<Muid, WorldObject> _units;
        Dictionary<Muid, WorldObject> _towers;

        public TeamType TeamType { get { return _teamType; } }
        public Architecture Architecture { get { return _architecture; } }
        public int CurrentUnitCount
        {
            get
            {
                if (_units == null)
                    return 0;
                return _units.Count;
            }
        }
        public int CurrentTowerCount
        {
            get
            {
                if (_towers == null)
                    return 0;
                return _towers.Count;
            }
        }

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

        public void Init(int id)
        {
            TeamRecord record = Game.Instance.DataTableManager.GameData.TeamRecord.TryGetValue(id);
            if(record == null)
            {
                Debug.LogError("해당 id 값의 데이터가 존재하지 않습니다 : " + id);
                return;
            }

            Name = record.Name;
            CurrentHP = record.StartHP;
            FullHP = record.FullHP;
            CurrentMP = record.StartMP;
            FullMP = record.FullMP;
            CurrentMoney = record.StartMoney;
            MaxUnitCount = record.MaxUnitCount;

            //todo MaxTowerCount

            Init((TeamType)record.TeamType);
        }

        public void AddTower(WorldObject obj)
        {
            if(obj.IsTower() == false)
                return;
            _towers.Add(obj.Muid, obj);
        }

        public void AddUnit(WorldObject obj)
        {
            if (obj.IsUnit() == false)
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

        public void DestroyTower(Muid muid)
        {
            WorldObject tower = _towers.TryGetValue(muid);
            if (tower == null)
                return;
            _towers.Remove(muid);
            Game.Instance.MemoryManager.Destroy(tower.gameObject);
        }
    }
}


