using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Core
{
    public static class MeshUtil
    { 
        public static void GetMinMaxPointWorld(Mesh mesh,Transform transform,out Vector3 minPoint, out Vector3 maxPoint)
        {
            Vector3[] worldVertices = new Vector3[mesh.vertexCount];
            Vector3[] vertices = mesh.vertices;
            for(int i =0; i < mesh.vertexCount; ++i)
            {
                worldVertices[i] = transform.localToWorldMatrix * new Vector4(vertices[i].x, vertices[i].y, vertices[i].z,1f);
            }

            minPoint = maxPoint = worldVertices[0];
            
            for (int i = 1; i < worldVertices.Length; ++i)
            {
                if (minPoint.x > worldVertices[i].x)
                    minPoint.x = worldVertices[i].x;
                if (minPoint.y > worldVertices[i].y)
                    minPoint.y = worldVertices[i].y;
                if (minPoint.z > worldVertices[i].z)
                    minPoint.z = worldVertices[i].z;

                if (maxPoint.x < worldVertices[i].x)
                    maxPoint.x = worldVertices[i].x;
                if (maxPoint.y < worldVertices[i].y)
                    maxPoint.y = worldVertices[i].y;
                if (maxPoint.z < worldVertices[i].z)
                    maxPoint.z = worldVertices[i].z;
            }
        }

    }
}