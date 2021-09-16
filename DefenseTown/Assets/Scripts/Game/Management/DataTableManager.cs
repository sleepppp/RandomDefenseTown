using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    public class DataTableManager
    {
        public GameData GameData;
        public ColorTableSO ColorTable;

        public void Init()
        {
            //todo GameData TSV�о� ���� ��� ����(���� ���� ���� ��Ʈ������ �дµ� Addressable�� �ε��ϰԲ� ó��)
            GameData = new GameData();

            LoadColorTable();
        }

        void LoadColorTable()
        { 
            //todo adressable�� �񵿱� �ۿ� ������ ���ϹǷ� �̷��� ó��
            string path = "ColorTable";
            ColorTable = Resources.Load<ColorTableSO>(path);
        }
    }
}
