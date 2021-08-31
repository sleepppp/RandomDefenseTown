using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data.Utility;
using Core.Data;

namespace My.Data
{
	public class GameData
	{
		public string TestPath = "Assets/Data/TSV/Test.tsv";
		public Dictionary<int,Test> Test;

		public GameData()
		{
			Test  =  TableStream.LoadTableByTSV(TestPath).TableToDictionary<int,Test>();
		}
	}
}
