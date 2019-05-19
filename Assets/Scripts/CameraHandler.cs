using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Range(0f, 1f)] public float speed = .1f;
    public GameController grid;

    private Vector3 _center;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCenter();
        CenterOnGrid();
    }

    void CalculateCenter()
    {
        float minX = 0, minZ = 0, maxX = 0, maxZ = 0;
        foreach (KeyValuePair<HexCoordinates, HexCell> entry in grid.Cells)
        {
            HexCell cell = entry.Value;
            Vector3 pos = cell.transform.position;
            if (pos.x < minX)
                minX = pos.x;
            else if (pos.x > maxX)
                maxX = pos.x;

            if (pos.z < minZ)
                minZ = pos.z;
            else if (pos.z > maxZ)
                maxZ = pos.z;
        }

        float x = (maxX - minX) / 2 + minX;
        float z = (maxZ - minZ) / 2 + minZ;
        _center = new Vector3(x, 0, z);
    }

    void CenterOnGrid()
    {
        Vector3 newPos = transform.position;

        newPos.x = Mathf.Lerp(newPos.x, _center.x, speed);
        newPos.z = Mathf.Lerp(newPos.z, _center.z, speed);
        transform.position = newPos;
    }
}