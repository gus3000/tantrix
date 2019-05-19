using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexCell : MonoBehaviour
{
    public int resolution = 50;
    public float lineWidth = .1f;
    public Material curveMaterial;
    public HexCoordinates coordinates;
    public Mesh[] curves;

    public TileInfo.LineColor[] SideColor { get; set; }

    public void InitCurves()
    {
//        SideColor = new TileInfo.LineColor[] {0, 0, 1, 2, 1, 2};
        Color[] colors = null;
        HexCurveHandler.GetCurves(this, ref curves, ref colors);

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            GameObject g = t.gameObject;
            if(g.CompareTag("Curve"))
                Destroy(g);
        }
        
        for(int i=0; i<3; i++)
        {
            GameObject g = new GameObject();
            g.tag = "Curve";

            g.transform.SetParent(transform, false);
            g.transform.position = Vector3.up * .01f;
            MeshFilter curveFilter = g.AddComponent<MeshFilter>();
            curveFilter.mesh = curves[i];

            MeshRenderer curveRenderer = g.AddComponent<MeshRenderer>();
            curveRenderer.material = new Material(curveMaterial);
            curveRenderer.material.color = colors[i];
        }
    }
}