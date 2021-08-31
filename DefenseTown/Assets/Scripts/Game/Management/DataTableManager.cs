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
            GameData = new GameData();

            //todo load ColorTable
            //string loadPath = "";
        }
    }
}
