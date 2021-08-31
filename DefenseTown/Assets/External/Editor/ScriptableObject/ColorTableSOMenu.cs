using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace My.Editor
{
    using My.Data;
    public class ColorTableSOMenu
    {
        [MenuItem("My/Create/ScriptableObject/ColorTableSO")]
        public static void CreateColorTable()
        {
            ColorTableSO table = ScriptableObject.CreateInstance<ColorTableSO>();
            table.name = "ColorTable";

            AssetDatabase.CreateAsset(table, "Assets/Deploy/ScriptableObjects/ColorTable.asset");
        }
    }
}
