using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    public class BuildTower : TowerBase
    {
        float _timer = 0f;

        private void Update()
        {
            _timer += Time.deltaTime;
            if(_timer >= _record.BuildingTime)
            {
                //todo Cell ÁöÁ¤
                Game.Instance.World.CreateTower(_record.ID, _centerCell, TeamType, IsPlayerOwner,(tower)=> 
                {

                });
            }
        }
    }
}
