using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    public class TowerBase : WorldObject
    {
        protected TowerRecord _record;
        public virtual void Init(int towerID,Cell targetCell)
        {
            _record = Game.Instance.DataTableManager.GameData.TowerRecord.TryGetValue(towerID);
            if (_record == null)
            {
                Debug.LogErrorFormat("{0} �ش� ID ���� ��� Ÿ���� ������ �� �����ϴ�");
                return;
            }

            transform.position = targetCell.CenterPos;

            Vector2Int index = targetCell.Index;
            Game.Instance.World.Grid.ConnectRangeToCell(index, _record.SizeX, _record.SizeY, this);
        }

        public void DestroyTower()
        {
            Vector2Int index = Grid.TransformPositionToIndex(transform.position);
            Game.Instance.World.Grid.DisConnectRangeToCell(index, _record.SizeX, _record.SizeY, this);
            Game.Instance.World.DestroyTower(this);
        }
    }
}