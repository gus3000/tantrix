using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Point
{
    [SerializeField] private Vector2 origin;
    [SerializeField] private Vector2 handle1;
    [SerializeField] private Vector2 handle2;

    public Point(Vector2 origin, Vector2 handle)
    {
        this.origin = origin;
        handle1 = handle;
        handle2 = 2 * origin - handle;
    }

    public Vector2 Origin => origin;

    public Vector2 Handle => handle1;
    public Vector2 SecondaryHandle => handle2;

    public void Move(Vector2 pos)
    {
        Vector2 diff = pos - origin;

        handle1 += diff;
        handle2 += diff;
        origin = pos;
    }

    public void MoveHandle(Vector2 pos)
    {
        Vector2 diff = pos - handle1;
        handle2 -= diff;
        handle1 = pos;
    }
}