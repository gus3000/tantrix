using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    public Material tileMaterial;
    private Mesh _hexMesh;
    private List<Vector3> _vertices;
    private List<int> _triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();
        _hexMesh.name = "Hex Mesh";

        GetComponent<MeshRenderer>().material = tileMaterial;
        
        _vertices = new List<Vector3>();
        _triangles = new List<int>();
    }

    public void Triangulate(HexCell[] cells)
    {
        _hexMesh.Clear();
        _vertices.Clear();
        _triangles.Clear();

        foreach (HexCell c in cells)
            Triangulate(c);

        _hexMesh.vertices = _vertices.ToArray();
        _hexMesh.triangles = _triangles.ToArray();
        _hexMesh.RecalculateNormals();
    }

    private void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;
        int startIndice = _vertices.Count;
        for (int i = 0; i < 6; ++i)
        {
            _vertices.Add(center + HexMetrics.corners[i]);
        }

        AddTriangle(startIndice, 0, 2, 4);
        AddTriangle(startIndice, 0, 1, 2);
        AddTriangle(startIndice, 2, 3, 4);
        AddTriangle(startIndice, 0, 4, 5);
    }

    private void AddTriangle(int startIndice, int v1, int v2, int v3)
    {
        _triangles.Add(startIndice + v1);
        _triangles.Add(startIndice + v2);
        _triangles.Add(startIndice + v3);
    }
}