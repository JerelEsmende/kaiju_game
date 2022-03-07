using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class PGC_Mesh : MonoBehaviour
{
    Mesh mesh;

    public Vector3[] vertices;
    public int[] triangles;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private int xSize = 50;
    private int zSize = 50;

    private float scale = 1.0f;
    float speed = 2.0f;

    private Vector3[] Height;
    
    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshRenderer.material = Resources.Load<Material>("MeshMaterial");
        meshCollider.sharedMesh = mesh;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
        UpdateMesh();
        
        //PrimCube();
        //CreateCube();
    }

    void Update()
    {
        WaveGeneration();
    }
    
    void GenerateMesh()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
            }
        }

        triangles = new int[xSize * zSize * 6];
        int ver = 0;
        int tri = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tri] = ver + 0;
                triangles[tri + 3] = triangles[tri + 2] = ver + 1;
                triangles[tri + 4] = triangles[tri + 1] = ver + xSize + 1;
                triangles[tri + 5] = ver + xSize + 2;
                tri += 6;
                ver++;
            }

            ver++;
        }
        
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
    
    void WaveGeneration()
    {
        if (Height == null)
            Height = mesh.vertices;

        Vector3[] vertices = new Vector3[Height.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = Height[i];
            //vertex.y += Mathf.Sin(Time.time * speed + Height[i].y + Height[i].z) * scale;
            vertex.y += Mathf.Sin(Time.time * speed + Height[i].z) * Mathf.Cos(Time.time * speed + Height[i].x + Height[i].z);
            //vertex.y += Mathf.Cos(Time.time * speed - Height[i].y + Height[i].z) * scale;
           // vertex.y += Mathf.Cos(Time.time * speed + Height[i].x + Height[i].z) * scale;

            vertices[i] = vertex;
        }


        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }


    void PrimCube()
    {
        GameObject Primcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Primcube.transform.position = new Vector3(20, 20, 20);
        Rigidbody rigidbody = Primcube.AddComponent<Rigidbody>();

    }
    
}

