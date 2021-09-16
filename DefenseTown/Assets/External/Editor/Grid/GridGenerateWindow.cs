using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace My.Editor
{
    using My.Data;
    using My.Game;
    public class GridGenerateWindow : EditorWindow
    {
        [MenuItem("My/Window/GridGenerate")]
        static void ShowWindow()
        {
            GridGenerateWindow win = GetWindow<GridGenerateWindow>();
        }

        public enum BrushType : int
        {
            None = 0,
            Moveable = 1,
            Block = 2,
        }

        Grid _grid;
        BrushType _brushType;

        List<Vector2Int> _pickIndexList = new List<Vector2Int>();
        bool _keyDown = false;
        int _brushSize = 1;

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            _grid = EditorGUILayout.ObjectField( _grid, typeof(Grid),true,null) as Grid;
            if (EditorGUI.EndChangeCheck())
            {
                if (_grid != null)
                {
                    _grid.EditorInit(_grid.GridSO);
                }
            }

            if (_grid == null)
            {
                ShowCreateButton();
            }
            else
            {
                EditGrid();
                UpdateMouseInput();
            }
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui -= OnSceneViewGUI;
            SceneView.duringSceneGui += OnSceneViewGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneViewGUI;

            ResetPick();
        }

        void OnSceneViewGUI(SceneView scene)
        {
            if (_grid == null)
                return;

            Event e = Event.current;

            Ray ray;
            Vector3 mousePosition = e.mousePosition;

            mousePosition.y = scene.camera.pixelHeight - mousePosition.y;
            ray = scene.camera.ScreenPointToRay(mousePosition);

            RaycastHit result;
            bool isPick = Grid.RaycastToGrid(scene.camera, mousePosition, out result);
            if (isPick == false)
            {
                ChangePickCell(-1, -1);
            }
            else
            {
                if(e.type == EventType.MouseMove)
                {
                    Vector2Int pickIndex = Grid.TransformPositionToIndex(result.point);
                    ChangePickCell(pickIndex.x, pickIndex.y);
                }
            }

        }

        void ShowCreateButton()
        {
            if (GUILayout.Button("Grid 생성"))
            {
                GridSO gridSO = GetSO();

                SetData(gridSO, GetTerrain());
                SetData(gridSO, GetWorldObjectsInScene());
                gridSO.Bake();

                string sceneName = EditorSceneManager.GetActiveScene().name;

                GameObject gridObject = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/External/Editor/Grid/Grid.prefab"));
                gridObject.name = sceneName + "Grid";
                Grid grid = gridObject.GetComponent<Grid>();
                grid.EditorInit(gridSO);
                _grid = grid;

                AssetDatabase.CreateAsset(gridSO, GridSO.CreatePath + sceneName + ".asset");
                AssetDatabase.Refresh();
            }
        }

        void EditGrid()
        {
            _brushSize = EditorGUILayout.IntField("BrushSize",_brushSize);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("BrushType");
                _brushType = (BrushType)EditorGUILayout.EnumPopup(_brushType);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("'B'를 누른 상태로 그리드에 브러쉬 하시오");
        }

        void UpdateMouseInput()
        {
            Event e = Event.current;

            if(e.isKey)
            {
                if (e.keyCode == KeyCode.B && e.rawType == EventType.KeyDown)
                {
                    _keyDown = true;
                    ApplyBrush();
                    e.Use();
                    //Debug.Log("keyDown");
                }
                else if (e.keyCode == KeyCode.B && e.rawType == EventType.KeyUp)
                {
                    _keyDown = false;
                    e.Use();
                    //Debug.Log("KeyUp");
                }
            }
            else if(e.type == EventType.MouseMove)
            {
                if (_keyDown)
                {
                    ApplyBrush();
                    e.Use();
                    //Debug.Log("keyMove");
                }
            }
        }

        void ApplyBrush()
        {
            CellType type = default;
            switch (_brushType)
            {
                case BrushType.Moveable:
                    type = CellType.Moveable;
                    break;
                case BrushType.Block:
                    type = CellType.Block;
                    break;
                default:
                    return;
            }

            for(int i =0; i < _pickIndexList.Count; ++i)
            {
                if (_grid.GridSO.IsOutOfRange(_pickIndexList[i].x, _pickIndexList[i].y)) continue;
                _grid.GridSO.SetCellType(_pickIndexList[i].x, _pickIndexList[i].y, type);
                SetColorAuto(_pickIndexList[i].x, _pickIndexList[i].y);
            }

        }

        void ChangePickCell(int centerIndexX, int centerIndexY)
        {
            List<Vector2Int> newPickList = new List<Vector2Int>();

            for(int y= centerIndexY - _brushSize; y <= centerIndexY + _brushSize; ++y)
            {
                for(int x= centerIndexX - _brushSize; x <= centerIndexX + _brushSize; ++x)
                {
                    if (_grid.GridSO.IsOutOfRange(x, y)) continue;
                    newPickList.Add(new Vector2Int(x, y));
                }
            }

            for(int i =0; i < _pickIndexList.Count; ++i)
            {
                CellType type = _grid.GridSO.GetCellType(_pickIndexList[i].x, _pickIndexList[i].y);
                Color color = ColorTableSO.LoadColorTable().GetColor(type);
                SetCellColor(_pickIndexList[i].x, _pickIndexList[i].y, color);
            }

            _pickIndexList =newPickList;

            for (int i = 0; i < _pickIndexList.Count; ++i)
            {
                SetCellColor(_pickIndexList[i].x, _pickIndexList[i].y, Color.blue);
            }
        }

        void SetColorAuto(int indexX, int indexY)
        {
            CellType type = _grid.GridSO.GetCellType(indexX, indexY);
            Color color = ColorTableSO.LoadColorTable().GetColor(type);
            SetCellColor(indexX, indexY, color);
        }

        void ResetPick()
        {
            for (int i = 0; i < _pickIndexList.Count; ++i)
            {
                CellType type = _grid.GridSO.GetCellType(_pickIndexList[i].x, _pickIndexList[i].y);
                Color color = ColorTableSO.LoadColorTable().GetColor(type);
                SetCellColor(_pickIndexList[i].x, _pickIndexList[i].y, color);
            }
            _pickIndexList.Clear();
        }

        void SetCellColor(int indexX, int indexY,Color color)
        {
            _grid.SetCellColor(indexX, indexY, color);
        }

        GridSO GetSO()
        {
            GridSO gridSO = AssetDatabase.LoadAssetAtPath<GridSO>(GridSO.CreatePath);
            if (gridSO == null)
            {
                gridSO = ScriptableObject.CreateInstance<GridSO>();
            }
            return gridSO;
        }

        GameObject GetTerrain()
        {
            return GameObject.Find("Terrain");
        }

        List<GameObject> GetWorldObjectsInScene()
        {
            Scene scene = EditorSceneManager.GetActiveScene();

            GameObject[] root = scene.GetRootGameObjects();

            List<GameObject> worldObjectList = new List<GameObject>();
            int worldObjectLayer = LayerMask.NameToLayer("WorldObject");
            for (int i = 0; i < root.Length; ++i)
            {
                root[i].FindObjectsWithLayer(worldObjectLayer, worldObjectList,(worldObject)=>
                {
                    return worldObject.GetComponent<MeshRenderer>() == null;
                });
            }

            return worldObjectList;
        }

        void SetData(GridSO gridSO,List<GameObject> worldObjectList)
        {
            for (int i = 0; i < worldObjectList.Count; ++i)
            {
                Vector3 min, max;
                MeshUtil.GetMinMaxPointWorld(worldObjectList[i].GetComponent<MeshFilter>().sharedMesh, worldObjectList[i].transform, out min, out max);

                Vector2Int minIndex = My.Game.Grid.TransformPositionToIndex(min);
                Vector2Int maxIndex = My.Game.Grid.TransformPositionToIndex(max);

                for (int y = minIndex.y; y <= maxIndex.y; ++y)
                {
                    for (int x = minIndex.x; x <= maxIndex.x; ++x)
                    {
                        gridSO.SetCellType(x, y, CellType.Block);
                    }
                }
            }
        }

        void SetData(GridSO gridSO,GameObject terrainObject)
        {
            Mesh mesh = null;
            float width = 0;
            float height = 0;
            bool isTerrain = false;
            if (terrainObject.GetComponent<MeshFilter>() != null)
            {
                mesh = terrainObject.GetComponent<MeshFilter>().sharedMesh;
                Vector3 min, max;
                MeshUtil.GetMinMaxPointWorld(mesh, terrainObject.transform, out min, out max);

                width = max.x - min.x;
                height = max.z - min.z;

                gridSO.Init((int)width, (int)height, Vector2.one);
            }
            else if (terrainObject.GetComponent<Terrain>())
            {
                Terrain terrain = terrainObject.GetComponent<Terrain>();
                TerrainData terrainData = terrain.terrainData;

                width = terrainData.size.x;
                height = terrainData.size.y;

                gridSO.Init((int)width, (int)height, Vector2.one);
                //todo 나무 월드 좌표 처리 제대로 안되고 있는 중. 확인 필요
                TreeInstance[] treeInstance = terrainData.treeInstances;
                for(int i =0; i < treeInstance.Length; ++i)
                {
                    Vector3 worldPosition = treeInstance[i].position;
                    worldPosition.x = worldPosition.x * width;
                    worldPosition.z = worldPosition.z * height;
                    Vector2Int index = My.Game.Grid.TransformPositionToIndex(worldPosition);
                    gridSO.SetCellType(index.x, index.y, CellType.Block);
                }
            }


        }

        bool IsContainPickList(int indexX, int indexY)
        {
            for(int i =0; i < _pickIndexList.Count; ++i)
            {
                if (_pickIndexList[i].x == indexX &&
                    _pickIndexList[i].y == indexY)
                    return true;
            }
            return false;
        }
    }
}