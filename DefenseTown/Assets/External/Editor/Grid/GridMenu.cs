using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
namespace My.Editor
{
    public class GridMenu
    {
        [MenuItem("My/Create/Prefab/Grid")]
        static void CreateGridMenu()
        {
            GameObject newObject = new GameObject();
            newObject.name = "Grid";
            MeshRenderer renderer = newObject.AddComponent<MeshRenderer>();
            MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
            MeshCollider collider = newObject.AddComponent<MeshCollider>();
            My.Game.Grid grid = newObject.AddComponent<My.Game.Grid>();

            meshFilter.mesh = My.Core.MeshGenerator.CreateQuad(Vector3.zero,Vector2.one,10);
            renderer.material = new Material(Shader.Find("Unlit/Grid"));
            collider.sharedMesh = meshFilter.mesh;

            grid.Init(10, 10);
            grid.SetGridColor(Color.yellow);
        }
    }
}
