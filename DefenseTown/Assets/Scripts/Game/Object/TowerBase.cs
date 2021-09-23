using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    public class TowerBase : WorldObject
    {
        protected TowerRecord _record;
        protected Cell _centerCell;
        public virtual void Init(int towerID,Cell targetCell)
        {
            _record = Game.Instance.DataTableManager.GameData.TowerRecord.TryGetValue(towerID);
            if (_record == null)
            {
                Debug.LogErrorFormat("{0} 해당 ID 값이 없어서 타워를 생성할 수 없습니다");
                return;
            }
            _centerCell = targetCell;
            transform.position = targetCell.CenterPos;

            ConnectToCell();
        }

        public void DestroyTower()
        {
            DisConnectToCell();
            Game.Instance.World.DestroyTower(this);
        }

        public void ConnectToCell()
        {
            Vector2Int index = _centerCell.Index;
            Game.Instance.World.Grid.ConnectRangeToCell(index, _record.SizeX, _record.SizeY, this);
        }

        public void DisConnectToCell()
        {
            Vector2Int index = _centerCell.Index;
            Game.Instance.World.Grid.DisConnectRangeToCell(index, _record.SizeX, _record.SizeY, this);
        }
    }
}