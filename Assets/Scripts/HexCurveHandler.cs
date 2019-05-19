using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public static class HexCurveHandler
{
    private const float verticalOffset = 0.01f;

//    public Vector2[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
//    {
//    }

    public static void GetCurves(HexCell cell, ref Mesh[] curves, ref Color[] colors)
    {
        List<int> colorsDone = new List<int>();
        List<Mesh> curveList = new List<Mesh>();
        colors = new Color[3];

        for (int i = 0; i < 6; ++i)
        {
            int firstSide = -1, secondSide = -1;
            int color = (int)cell.SideColor[i];

            firstSide = i;

            if (colorsDone.Contains(color))
                continue;
            colorsDone.Add(color);
            colors[curveList.Count] = TileInfo.ColorFromInt[color];
            for (int j = i + 1; j < 6; ++j)
                if ((int)cell.SideColor[j] == color)
                {
                    secondSide = j;
                    break;
                }

            //Si on est toujours là, il faut tracer la ligne entre firstSide et secondSide
            Mesh mesh;
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            var cellPosition = cell.transform.position;
            Vector3 firstSidePos = cellPosition + HexMetrics.sides[firstSide];
            Vector3 secondSidePos = cellPosition + HexMetrics.sides[secondSide];

//            Material m = new Material(cell.curveMaterial);
//            m.color = colorFromInt[color];
//
//            GameObject start = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            start.transform.position = firstSidePos;
//            start.transform.localScale = Vector3.one * .1f;
//            start.GetComponent<MeshRenderer>().material = m;
//
//            GameObject end = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            end.transform.position = secondSidePos;
//            end.transform.localScale = Vector3.one * .1f;
//            end.GetComponent<MeshRenderer>().material = m;

            float increment = 1f / cell.resolution;
            List<Vector3> positions = new List<Vector3>();

            Vector3 pos;
            int baseIndex;
            Vector3 direction;
            Vector3 right;

//            Vector3 controlPoint = Vector3.Lerp(Vector3.Lerp(firstSidePos, secondSidePos, .5f), cellPosition, .5f);
            Vector3 controlPoint = cellPosition + Vector3.up * verticalOffset * colorsDone.Count;
//            GameObject controlPointMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//            controlPointMarker.transform.position = controlPoint;
//            controlPointMarker.transform.localScale = Vector3.one * .1f;
//            controlPointMarker.GetComponent<MeshRenderer>().material = m;
            
            for (float t = 0; t < 1; t += increment)
            {
                pos = CurvesUtil.EvaluateQuadratic(firstSidePos, controlPoint, secondSidePos, t);

                baseIndex = vertices.Count;
                if (positions.Count > 0)
                {
                    direction = (pos - positions[positions.Count - 1]).normalized;
                }
                else
                {
                    direction = (cellPosition - firstSidePos).normalized;
                }

                right = Vector3.Cross(direction, cell.transform.up).normalized;

                vertices.Add(pos + right * cell.lineWidth);
                vertices.Add(pos - right * cell.lineWidth);

                AddTriangle(ref triangles, baseIndex, 0, 2, 1);
                AddTriangle(ref triangles, baseIndex, 2, 3, 1);

                positions.Add(pos);

//                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//                g.transform.position = pos;
//                g.transform.localScale = Vector3.one * cell.lineWidth;
//                g.GetComponent<MeshRenderer>().material = m;
            }

            //curve end
            pos = CurvesUtil.EvaluateQuadratic(firstSidePos, cellPosition, secondSidePos, 1);
            baseIndex = vertices.Count;
            direction = (pos - positions[positions.Count - 1]).normalized;
            right = Vector3.Cross(direction, cell.transform.up).normalized;
            vertices.Add(pos + right * cell.lineWidth);
            vertices.Add(pos - right * cell.lineWidth);

            //we construct the mesh
            mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            curveList.Add(mesh);
        }

//        Debug.Log(string.Join(",", colorsDone));

        curves = curveList.ToArray();
    }

    private static void AddTriangle(ref List<int> triangles, int startIndice, int v1, int v2, int v3)
    {
        triangles.Add(startIndice + v1);
        triangles.Add(startIndice + v2);
        triangles.Add(startIndice + v3);
    }
}