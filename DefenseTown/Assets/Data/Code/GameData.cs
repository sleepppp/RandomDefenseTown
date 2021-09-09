using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace My.Data
{
	public class GameData
	{
		public string TeamRecordPath = "Assets/Data/TSV/TeamRecord.tsv";
		public Dictionary<int,TeamRecord> TeamRecord;

		public GameData()
		{
			TeamRecord  =  TableStream.LoadTableByTSV(TeamRecordPath).TableToDictionary<int,TeamRecord>();
		}
	}
}
