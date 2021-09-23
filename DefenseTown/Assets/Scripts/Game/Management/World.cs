using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace My.Game
{
    using My.Data;
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

        public Dictionary<TeamType,Team>.ValueCollection GetAllTeam()
        {
            return _teams.Values;
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

        public void CreateBuildTower(int towerID, Cell targetCell ,TeamType teamType,bool isPlayerOwner,Action<TowerBase> callback)
        {
            TowerRecord record = Game.Instance.DataTableManager.GameData.TowerRecord.TryGetValue(towerID);
            if(record == null)
            {
                Debug.LogErrorFormat("{0}의 타워 ID값이 없어서 생성할 수 없습니다",towerID);
                return;
            }
            string towerAssetPath = "Assets/Deploy/Game/WorldObject/Tower/" + record.BuildTowerPrefabName + ".prefab";
            
            AssetManager.CreatePrefabAsync(towerAssetPath, (newObject) =>
            {
                //todo 부모 지정
                TowerBase tower = newObject.GetComponent<TowerBase>();
                tower.DynamicInit(WorldObjectType.Tower, teamType, isPlayerOwner);
                tower.Init(record.ID, targetCell);

                GetTeam(teamType).AddTower(tower);

                callback?.Invoke(tower);
            });
        }

        public void CreateTower(int towerID,Cell targetCell, TeamType teamType,bool isPlayerOwner,Action<TowerBase> callback)
        {
            TowerRecord record = Game.Instance.DataTableManager.GameData.TowerRecord.TryGetValue(towerID);
            if (record == null)
            {
                Debug.LogErrorFormat("{0}의 타워 ID값이 없어서 생성할 수 없습니다",towerID);
                return;
            }

            string towerAssetPath = "Assets/Deploy/Game/WorldObject/Tower/" + record.TowerPrefabName + ".prefab";
            AssetManager.CreatePrefabAsync(towerAssetPath, (newObject) =>
            {
                //새로 생성전에 기존에 있던 BuildTower는 제거해 주어야 한다.
                TowerBase prevTower = targetCell.GetTower();
                if(prevTower != null)
                {
                    prevTower.DestroyTower();
                }

                //todo 부모지정
                TowerBase tower = newObject.GetComponent<TowerBase>();
                tower.DynamicInit(WorldObjectType.Tower, teamType, isPlayerOwner);
                tower.Init(record.ID, targetCell);

                GetTeam(teamType).AddTower(tower);

                callback?.Invoke(tower);
            });
        }

        public void DestroyTower(WorldObject worldObject)
        {
            GetTeam(worldObject.TeamType).DestroyTower(worldObject.Muid);
        }

        public WorldObject GetPickObject()
        {
            return _pickSystem.PickObject;
        }
    }
}


