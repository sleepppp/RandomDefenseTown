using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My
{
    public static class MeshGenerator
    {
        /*
         * 2 3
         * 0 1
         */
        public static Mesh CreateQuad(Vector3 startPosition, Vector3 size,float scale = 1)
        {
            Mesh mesh = new Mesh();
            mesh.name = "Quad";
            Vector3[] vertices = new Vector3[4];
            vertices[0] = startPosition + new Vector3(0f, 0f, 0f) * scale;
            vertices[1] = startPosition + new Vector3(size.x, 0f, 0f) * scale;
            vertices[2] = startPosition + new Vector3(0f, 0f, size.y) * scale;
            vertices[3] = startPosition + new Vector3(size.x, 0f, size.y) * scale;

            int[] indices = new int[6];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 3;

            indices[3] = 0;
            indices[4] = 3;
            indices[5] = 1;

            Vector2[] uvs = new Vector2[4];
            uvs[0] = new Vector2(0f, 0f);
            uvs[1] = new Vector2(1f, 0f);
            uvs[2] = new Vector2(0f, 1f);
            uvs[3] = new Vector2(1f, 1f);

            mesh.vertices = vertices;
            mesh.triangles = indices;
            mesh.uv = uvs;

            return mesh;
        }
    }
}