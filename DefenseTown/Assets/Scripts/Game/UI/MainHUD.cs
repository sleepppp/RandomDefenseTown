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

        public void Init()
        {
            RefreshAll();
        }

        public void RefreshAll()
        {
            RefreshPlayerInfo();
        }

        public void RefreshPlayerInfo()
        {
            Team playerTeam = Game.Instance.World.GetTeam(TeamType.PlayerTeam);
            _moneyCountText.text = playerTeam.CurrentMoney.ToString();
            _towerCountText.text = string.Format("{0} / {1}", playerTeam.CurrentTowerCount, playerTeam.MaxTowerCount);
            _unitCountText.text = string.Format("{0} / {1}", playerTeam.CurrentUnitCount, playerTeam.MaxUnitCount);
        }
    }
}