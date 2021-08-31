using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Data
{
    public class ColorTableSO : ScriptableObject
    {
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
    }
}
