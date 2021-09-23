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

            //todo ������ ����� �� �� ����
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
                Debug.LogErrorFormat("{0}�� Ÿ�� ID���� ��� ������ �� �����ϴ�",towerID);
                return;
            }
            string towerAssetPath = "Assets/Deploy/Game/WorldObject/Tower/" + record.BuildTowerPrefabName + ".prefab";
            
            AssetManager.CreatePrefabAsync(towerAssetPath, (newObject) =>
            {
                //todo �θ� ����
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
                Debug.LogErrorFormat("{0}�� Ÿ�� ID���� ��� ������ �� �����ϴ�",towerID);
                return;
            }

            string towerAssetPath = "Assets/Deploy/Game/WorldObject/Tower/" + record.TowerPrefabName + ".prefab";
            AssetManager.CreatePrefabAsync(towerAssetPath, (newObject) =>
            {
                //���� �������� ������ �ִ� BuildTower�� ������ �־�� �Ѵ�.
                TowerBase prevTower = targetCell.GetTower();
                if(prevTower != null)
                {
                    prevTower.DestroyTower();
                }

                //todo �θ�����
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


