using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace My.Editor
{
    using My.Core;
    using My.Data;
    using My.Game;
    public class GridGenerateWindow : EditorWindow
    {
        [MenuItem("My/Window/GridGenerate")]
        static void ShowWindow()
        {
            GridGenerateWindow win = GetWindow<GridGenerateWindow>();
        }

        Grid _grid;

        private void OnGUI()
        {
            _grid = EditorGUILayout.ObjectField( _grid, typeof(Grid),true,null) as Grid;

            if(_grid == null)
            {
                if (GUILayout.Button("Grid 积己"))
                {
                    ShowCreateButton();
                }
            }
            else
            {

            }
        }

        void ShowCreateButton()
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
            Vector3 min, max;
            Mesh mesh = null;
            if (terrainObject.GetComponent<MeshFilter>() != null)
                mesh = terrainObject.GetComponent<MeshFilter>().sharedMesh;
            else if (terrainObject.GetComponent<Terrain>())
            {
                //todo Terrain 贸府
                //Terrain terrain = terrainObject.GetComponent<Terrain>();
                //terrain.
            }

            MeshUtil.GetMinMaxPointWorld(mesh, terrainObject.transform, out min, out max);

            float width = max.x - min.x;
            float height = max.z - min.z;

            gridSO.Init((int)width,(int)height, Vector2.one);
        }
    }
}