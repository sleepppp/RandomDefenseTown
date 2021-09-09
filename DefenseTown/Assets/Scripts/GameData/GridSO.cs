using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace My.Data
{
    public class GridSO : ScriptableObject
    {
        public const string CreatePath = "Assets/Deploy/ScriptableObjects/Grid/";

        public int CellWidthCount;
        public int CellHeightCount;
        public Vector2 CellSize;
        public My.Game.CellType[] CellTypes;
        Texture2D _bakedTexture;

        public Texture2D GetTexture()
        {
            if (_bakedTexture == null)
                Bake();

            return _bakedTexture;
        }

        public void Init(int cellWidthCount, int cellHeightCounnt, Vector2 cellSize)
        {
            CellWidthCount = cellWidthCount;
            CellHeightCount = cellHeightCounnt;
            CellSize = cellSize;
            CellTypes = new My.Game.CellType[CellHeightCount * CellWidthCount];
            int index = 0;
            for (int y = 0; y < CellHeightCount; ++y)
            {
                for(int x=0;x < CellWidthCount;++x)
                {
                    CellTypes[index] = My.Game.CellType.Moveable;
                    ++index;
                }
            }
        }

        public void SetCellType(int indexX, int indexY,My.Game.CellType cellType)
        {
            int index = CellWidthCount * indexY + indexX;
            if (index < 0 || index >= CellTypes.Length)
                return;
            CellTypes[index] = cellType;
        }

        public My.Game.CellType GetCellType(int indexX, int indexY)
        {
            int index = CellWidthCount * indexY + indexX;
            if (index < 0 || index >= CellTypes.Length)
                return default;
            return CellTypes[index];
        }

        public void SetCellColor(int indexX, int indexY, Color color)
        {
            if (_bakedTexture == null)
                return;

            _bakedTexture.SetPixel(indexX, indexY, color);
            _bakedTexture.Apply();
        }

        public bool IsOutOfRange(int indexX, int indexY)
        {
            if (indexX < 0 || indexX >= CellWidthCount) return true;
            if (indexY < 0 || indexY >= CellHeightCount) return true;
            return false;
        }

        public void Bake()
        {
            ColorTableSO colorTable = ColorTableSO.LoadColorTable();

            _bakedTexture = new Texture2D(CellWidthCount, CellHeightCount, TextureFormat.RGB24, false);
            _bakedTexture.filterMode = FilterMode.Point;
            _bakedTexture.wrapMode = TextureWrapMode.Clamp;
            
            int index = 0;
            for(int y =0; y < CellHeightCount; ++y)
            {
                for(int x=0;x < CellWidthCount; ++x)
                {
                    ColorTableSO.ColorDesc colorDesc = CellTypes[index] == Game.CellType.Moveable ? ColorTableSO.ColorDesc.Cell_Moveable : ColorTableSO.ColorDesc.Cell_Block;
                    Color color = colorTable.GetColor(colorDesc);

                    _bakedTexture.SetPixel(x, y, color);
            
                    ++index;
                }
            }

            _bakedTexture.Apply();
        }
    }
}
