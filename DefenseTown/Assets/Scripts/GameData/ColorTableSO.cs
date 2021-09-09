using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace My.Data
{
    public class ColorTableSO : ScriptableObject
    {
#if UNITY_EDITOR
        public static ColorTableSO LoadColorTable()
        {
            if(_instance == null)
            {
                _instance = AssetDatabase.LoadAssetAtPath<ColorTableSO>("Assets/Deploy/Resources/ColorTable.asset");
            }
            return _instance;
        }

        public static ColorTableSO _instance;
#endif
        public enum ColorDesc : int
        {
            Cell_Moveable = 0,
            Cell_Block = 1,
        }

        [SerializeField]Color[] _colorList;

        public Color GetColor(ColorDesc desc)
        {
            return _colorList[(int)desc];
        }

        public Color GetColor(My.Game.CellType type)
        {
            ColorDesc desc = ColorDesc.Cell_Moveable;
            switch(type)
            {
                case Game.CellType.Moveable:
                    desc = ColorDesc.Cell_Moveable;
                    break;
                case Game.CellType.Block:
                    desc = ColorDesc.Cell_Block;
                    break;
            }

            return GetColor(desc);
        }


    }
}
