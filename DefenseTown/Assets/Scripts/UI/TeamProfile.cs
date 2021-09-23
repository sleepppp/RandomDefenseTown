using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI
{
    using My.Game;
    public class TeamProfile : MonoBehaviour
    {
        [SerializeField] Image _profileImage;
        [SerializeField] Slider _hpSlider;
        [SerializeField] Slider _mpSlider;

        TeamType _teamType;

        public void Init(TeamType teamType)
        {
            _teamType = teamType;
            Refresh();
        }

        public void Refresh()
        {
            Team team = Game.Instance.World.GetTeam(_teamType);
            if(team == null)
            {
                Debug.LogError("해당 타입의 팀이 없습니다");
                return;
            }

            if(_profileImage.sprite == null)
            {
                //todo image불러오기
            }

            _hpSlider.value = team.CurrentHP / team.FullHP;
            _mpSlider.value = team.CurrentMP / team.FullMP;
        }
    }
}