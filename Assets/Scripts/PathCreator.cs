﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [HideInInspector] public Path path;

    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    private void Reset()
    {
        Debug.Log("RESET");
        
        path = new Path(transform.position);
    }
}