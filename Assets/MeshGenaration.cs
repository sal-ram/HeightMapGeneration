using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenaration : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    int sizeX = 20;
    int sizeZ = 20;

    List<List<float>>heightmap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateHeightMap();
        CreateShape();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        UpdateMesh();
    }

    private void Update()
    {
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    //заполн€ю компоненты vertices и triangles
    void CreateShape()
    {
        int i = 0;
        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];
        for (int z = 0; z <= sizeZ; z++)
        {
            for (int x = 0; x <= sizeX; x++)
            {
                vertices[i] = new Vector3(x, heightmap[x][z], z);
                i++;
            }
        }

        triangles = new int[sizeX * sizeZ * 6];
        int tris = 0;
        int j = 0;

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                triangles[tris] = j;
                triangles[tris + 1] = sizeX + j + 1;
                triangles[tris + 2] = j + 1;
                triangles[tris + 3] = j + 1;
                triangles[tris + 4] = sizeX + j + 1;
                triangles[tris + 5] = sizeX + j + 2;
                tris += 6;
                j++;

            }
            j++;
        }

    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    //генерирую случайный двухмерный массив - карта высот

    void GenerateHeightMap()
    {
        heightmap = new List<List<float>>();

        for (int x = 0; x <= sizeX; x++)
        {
            heightmap.Add(new List<float>());

            for (int z = 0; z <= sizeZ; z++)
            {
                heightmap[x].Add(Random.Range(-1f, 1f));
            }

        }

    }
}
