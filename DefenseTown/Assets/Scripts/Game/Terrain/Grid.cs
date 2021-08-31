using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    [RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
    public class Grid : MonoBehaviour
    {
        static readonly int _mainColorKey = Shader.PropertyToID("_MainColor");
        static readonly int _seconderyColorKey = Shader.PropertyToID("_SecondaryColor");
        static readonly int _maskTextureKey = Shader.PropertyToID("_MaskTexture");
        static readonly int _showCellStateKey = Shader.PropertyToID("_isShowCellState");

        Cell[,] _cellList;
#if UNITY_EDITOR
        [SerializeField] bool _isTest = false;
#endif
        [SerializeField]int _cellWidthCount;
        [SerializeField]int _cellHeightCount;

        Material _gridMaterial;
        MeshRenderer _meshRenderer;

        Texture2D _cellDataTexture;
#if UNITY_EDITOR
        private void Start()
        {
            if(_isTest)
            {
                TestInit(_cellWidthCount, _cellHeightCount);

                //Test~
                SetCellState(_cellWidthCount / 2, _cellHeightCount / 2, CellState.Block, true);
                ShowCellState(true);
            }
        }
#endif
        //private func~

        //public func~

        public void TestInit(int cellWidthCount, int cellHeightCount)
        {
            _cellWidthCount = cellWidthCount;
            _cellHeightCount = cellHeightCount;

            Init();

            _cellList = new Cell[cellHeightCount, cellWidthCount];
            for(int y=0; y < cellHeightCount; ++y)
            {
                for(int x=0;x < cellWidthCount; ++x)
                {
                    Cell cell = new Cell();
                    cell.Init
                        (
                        new Vector3(x, 0f, y),
                        Vector2.one,
                        CellType.Moveable
                        );

                    _cellList[y, x] = cell;
                    SetCellState(x, y, CellState.Normal, false);
                }
            }
            _cellDataTexture.Apply();
            _gridMaterial.SetTexture(_maskTextureKey, _cellDataTexture);
        }

        public void Init()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _gridMaterial = _meshRenderer.material;

            _cellDataTexture = new Texture2D(_cellWidthCount, _cellHeightCount, TextureFormat.RGB24, false);
            _cellDataTexture.filterMode = FilterMode.Point;
            _cellDataTexture.wrapMode = TextureWrapMode.Clamp;
            _cellDataTexture.Apply();
        }

        public void ShowGrid(bool bShow)
        {
            _meshRenderer.enabled = bShow;
        }

        public void ShowCellState(bool bShow)
        {
            if(bShow)
                _gridMaterial.SetFloat(_showCellStateKey, 1f);
            else
                _gridMaterial.SetFloat(_showCellStateKey, 0f);
        }

        public void SetGridColor(Color color)
        {
            _gridMaterial.SetColor(_mainColorKey, color);
            _gridMaterial.SetColor(_seconderyColorKey, color);
        }

        public void SetCellState(int indexX, int indexY,CellState state, bool bAutoApplyTexture = true)
        {
            if (IsOutOfRange(indexX, indexY))
                return;

            _cellList[indexY, indexX].State = state;
            Color cellColor = _cellList[indexY, indexX].CanPass ? Color.green : Color.red;
            _cellDataTexture.SetPixel(indexX, indexY, cellColor);
            //todo ColorTable 정의되면 변경
            //ColorTableSO.ColorDesc colorDesc = _cellList[indexY, indexX].CanPass ? ColorTableSO.ColorDesc.Cell_Moveable : ColorTableSO.ColorDesc.Cell_Block;
            //_cellDataTexture.SetPixel(indexX, indexY,Game.Instance.DataTableManager.ColorTable.GetColor(colorDesc));

            if (bAutoApplyTexture)
            {
                _cellDataTexture.Apply();
            }
        }

        public bool IsOutOfRange(int indexX, int indexY)
        {
            if (indexX < 0 || indexX >= _cellWidthCount) return true;
            if (indexY < 0 || indexY >= _cellHeightCount) return true;
            return false;
        }

        public Cell GetCell(int indexX, int indexY)
        {
            if (IsOutOfRange(indexX, indexY))
                return null;

            return _cellList[indexY, indexX];
        }

        public Cell GetCell(Vector3 position)
        {
            Vector2Int index = TransformPositionToIndex(position);
            if (IsOutOfRange(index.x, index.y))
                return null;
            return GetCell(index.x, index.y);
        }

        public static Vector2Int TransformPositionToIndex(Vector3 position)
        {
            Vector2Int result = new Vector2Int();
            result.x = (int)position.x;
            result.y = (int)position.z;
            return result;
        }
    }
}
