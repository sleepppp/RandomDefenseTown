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
            //todo GameData TSV읽어 오는 방법 수정(현재 직접 파일 스트림으로 읽는데 Addressable로 로드하게끔 처리)
            GameData = new GameData();

            LoadColorTable();
        }

        void LoadColorTable()
        { 
            //todo adressable이 비동기 밖에 지원을 안하므로 이렇게 처리
            string path = "ColorTable";
            ColorTable = Resources.Load<ColorTableSO>(path);
        }
    }
}
