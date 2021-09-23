using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI
{
    using My.Game;
    public class MainHUD : UIBase
    {
        [Header("PlayerInfo")]
        [SerializeField] Text _moneyCountText;
        [SerializeField] Text _towerCountText;
        [SerializeField] Text _unitCountText;
        [Header("TeamProfile")]
        [SerializeField] Transform _teamProfileGroup;

        Dictionary<TeamType, TeamProfile> _teamProfileList = new Dictionary<TeamType, TeamProfile>();

        public void Init()
        {
            CreateTeamProfile();

            Refresh();
        }

        public void Refresh()
        {
            RefreshPlayerInfo();
            RefreshTeamProfile();
        }

        public void RefreshPlayerInfo()
        {
            Team playerTeam = Game.Instance.World.GetTeam(TeamType.PlayerTeam);
            _moneyCountText.text = playerTeam.CurrentMoney.ToString();
            _towerCountText.text = string.Format("{0} / {1}", playerTeam.CurrentTowerCount, playerTeam.MaxTowerCount);
            _unitCountText.text = string.Format("{0} / {1}", playerTeam.CurrentUnitCount, playerTeam.MaxUnitCount);
        }

        public void RefreshTeamProfile()
        {
            foreach(var item in _teamProfileList)
            {
                item.Value.Refresh();
            }
        }

        public void RefreshTeamInfo(TeamType teamType)
        {
            TeamProfile profile = _teamProfileList.TryGetValue(teamType);
            profile?.Refresh();
        }

        void CreateTeamProfile()
        {
            AssetManager.LoadAssetAsync<GameObject>("Assets/Deploy/Game/UI/TeamProfile.prefab", (prefab) =>
            {
                Dictionary<TeamType, Team>.ValueCollection teamList = Game.Instance.World.GetAllTeam();
                foreach (var team in teamList)
                {
                    GameObject newObject = Game.Instance.MemoryManager.Instantiate(prefab, _teamProfileGroup);
                    TeamProfile teamProfile = newObject.GetComponent<TeamProfile>();
                    teamProfile.Init(team.TeamType);
                    _teamProfileList.Add(team.TeamType, teamProfile);
                }
            });
        }
    }
}