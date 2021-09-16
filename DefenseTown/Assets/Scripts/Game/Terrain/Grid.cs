using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Game
{
    using My.Data;
    [RequireComponent(typeof(MeshRenderer),typeof(MeshFilter),typeof(MeshCollider))]
    public class Grid : MonoBehaviour
    {
        static readonly int _mainColorKey = Shader.PropertyToID("_MainColor");
        static readonly int _seconderyColorKey = Shader.PropertyToID("_SecondaryColor");
        static readonly int _maskTextureKey = Shader.PropertyToID("_MaskTexture");
        static readonly int _showCellStateKey = Shader.PropertyToID("_isShowCellState");
        static readonly int _GraduationScaleXKey = Shader.PropertyToID("_GraduationScaleX");
        static readonly int _GraduationScaleYKey = Shader.PropertyToID("_GraduationScaleY");

        Cell[,] _cellList;
        int _cellWidthCount;    
        int _cellHeightCount;   
        [SerializeField] GridSO _gridSO;

        Material _gridMaterial;
        MeshRenderer _meshRenderer;

        Texture2D _cellDataTexture;

        PathFinder _pathFinder;

        public int CellWidthCount { get { return _cellWidthCount; } }
        public int CellHeightCount { get { return _cellHeightCount; } }
        public PathFinder PathFinder { get { return _pathFinder; } }

        public GridSO GridSO { get { return _gridSO; } }

        public Material SharedMaterial
        {
            get
            {
                if(_gridMaterial)
                {
                    _gridMaterial = GetComponent<MeshRenderer>().sharedMaterial;
                }
                return _gridMaterial;
            }
        }

        private void Start()
        {
            Init(_gridSO);
        }
#if UNITY_EDITOR
        public void EditorInit(GridSO gridSO)
        {
            _gridSO = gridSO;
            _cellWidthCount = gridSO.CellWidthCount;
            _cellHeightCount = gridSO.CellHeightCount;

            Mesh mesh = MeshGenerator.CreateQuad(Vector3.zero, Vector3.one, 10);
            GetComponent<MeshFilter>().sharedMesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
           _gridMaterial = new Material(Shader.Find("Unlit/Grid2"));
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.sharedMaterial = _gridMaterial;
            _cellDataTexture = gridSO.GetTexture();
            CalcShaderProperties();
            BindMaskTexture();
            ShowGrid(true);
            ShowCellState(true);
            SetGridColor(Color.yellow);

            gameObject.layer = LayerMask.NameToLayer("Grid");

            //todo 추 후 수정
            transform.position = new Vector3(transform.position.x, 0.1f,transform.position.z);
        }
#endif
        public void Init(GridSO gridSO)
        {
            _cellWidthCount = gridSO.CellWidthCount;
            _cellHeightCount = gridSO.CellHeightCount;

            _meshRenderer = GetComponent<MeshRenderer>();
            _gridMaterial = _meshRenderer.material;
            _cellDataTexture = _gridSO.GetTexture();

            CreateCellList();
            BindMaskTexture();
            CalcShaderProperties();

            for (int y=0;y < _cellHeightCount; ++y)
            {
                for(int x=0;x < _cellWidthCount; ++x)
                {
                    SetCellState(x,y, gridSO.GetCellType(x,y) == CellType.Block ? CellState.Block : CellState.Normal, false);
                }
            }
            _cellDataTexture.Apply();

            _pathFinder = new PathFinder();
            _pathFinder.Init(this);
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
            if (IsOutOfRange(indexX, indexY) || _cellList == null)
                return;

            _cellList[indexY, indexX].State = state;
            ColorTableSO.ColorDesc colorDesc = _cellList[indexY, indexX].CanPass ? ColorTableSO.ColorDesc.Cell_Moveable : ColorTableSO.ColorDesc.Cell_Block;
            _cellDataTexture.SetPixel(indexX, indexY,Game.Instance.DataTableManager.ColorTable.GetColor(colorDesc));

            if (bAutoApplyTexture)
            {
                _cellDataTexture.Apply();
            }
        }

        public void SetCellColor(int indexX, int indexY, Color color,bool bAutoApply = true)
        {
            if (IsOutOfRange(indexX, indexY))
                return;

            _cellDataTexture.SetPixel(indexX, indexY, color);

            if(bAutoApply)
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

        public Vector3 GetPositionContainY(Vector3 position)
        {
            Ray ray = new Ray();
            ray.origin = position;
            ray.direction = Vector3.down;

            int terrainMask = 1 << LayerMask.NameToLayer("Terrain");

            RaycastHit result;
            if (Physics.Raycast(ray, out result, 10f, terrainMask))
                return result.point;
            else
                return position;
        }

        public void ConnectToCell(Vector2Int index,WorldObject worldObject)
        {
            GetCell(index.x,index.y)?.Connect(worldObject);
        }

        public void ConnectRangeToCell(Vector2Int centerIndex,int sizeX, int sizeY,WorldObject worldObject)
        {
            for(int y= centerIndex.y - sizeY / 2; y <= centerIndex.y + sizeY / 2; ++y )
            {
                for(int x= centerIndex.x - sizeX / 2; x <= centerIndex.x + sizeX / 2; ++x)
                {
                    GetCell(centerIndex.x, centerIndex.y)?.Connect(worldObject);
                }
            }
        }

        public void DisConnectToCell(Vector2Int index,WorldObject worldObject)
        {
            GetCell(index.x, index.y)?.DisConnect(worldObject);
        }

        public void DisConnectRangeToCell(Vector2Int centerIndex, int sizeX, int sizeY, WorldObject worldObject)
        {
            for (int y = centerIndex.y - sizeY / 2; y <= centerIndex.y + sizeY / 2; ++y)
            {
                for (int x = centerIndex.x - sizeX / 2; x <= centerIndex.x + sizeX / 2; ++x)
                {
                    GetCell(centerIndex.x, centerIndex.y)?.DisConnect(worldObject);
                }
            }
        }

        public void CalcShaderProperties()
        {
            int widthMulCount = _cellWidthCount / 10;
            int heightMulCount = _cellHeightCount / 10;

            transform.localScale = new Vector3(widthMulCount, 1f, heightMulCount);
            _gridMaterial.SetFloat(_GraduationScaleXKey, widthMulCount);
            _gridMaterial.SetFloat(_GraduationScaleYKey, heightMulCount);
        }

        void BindMaskTexture()
        {
            _gridMaterial.SetTexture(_maskTextureKey, _cellDataTexture);
        }

        void CreateCellList()
        {
            _cellList = new Cell[_cellHeightCount, _cellWidthCount];
            for (int y = 0; y < _cellHeightCount; ++y)
            {
                for (int x = 0; x < _cellWidthCount; ++x)
                {
                    Cell cell = new Cell();
                    cell.Init
                        (
                        new Vector3(x, 0f, y),
                        new Vector2Int(x,y),
                        Vector2.one,
                        CellType.Moveable
                        );

                    _cellList[y, x] = cell;
                }
            }
        }

        public static Vector2Int TransformPositionToIndex(Vector3 position)
        {
            Vector2Int result = new Vector2Int();
            result.x = (int)position.x;
            result.y = (int)position.z;
            return result;
        }

        public static bool RaycastToGrid(Camera camera, Vector3 mousePoint, out RaycastHit result)
        {
            Ray ray = camera.ScreenPointToRay(mousePoint);

            if (Physics.Raycast(ray, out result, 100f, 1 << LayerMask.NameToLayer("Grid")))
            {
                return true;
            }
            return false;
        }
    }
}
