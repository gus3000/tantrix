﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [Serializable]
public class Path : IEnumerable<Point>
{
    [SerializeField]
    private List<Point> points;


    public Path(Vector2 center)
    {
        Vector2 v1 = center + Vector2.left;
        Vector2 v2 = center + Vector2.right;
        Vector2 delta = Vector2.left + Vector2.down;

        Point p1 = new Point(v1, v1 + delta);
        Point p2 = new Point(v2, v2 + delta);

        points = new List<Point> {p1, p2};
    }

    public Point this[int i]
    {
        get => points[i];
        set => points[i] = value;
    }

    public int NumPoints => points.Count;
    public int NumSegments => points.Count - 1;
    public Point LastPoint => points[points.Count - 1];

    public void AddPoint(Point p)
    {
        points.Add(p);
    }

    public void AddPoint(Vector2 v)
    {
        Vector2 handle = (v + LastPoint.SecondaryHandle) * 0.5f;
        points.Add(new Point(v, handle));
    }

    public void MovePoint(int pointIndex, Vector2 pos)
    {
        points[pointIndex].Move(pos);
    }

    public void MoveHandle(int pointIndex, Vector2 pos)
    {
        points[pointIndex].MoveHandle(pos);
    }


    public IEnumerator<Point> GetEnumerator()
    {
        return new PointEnum(points);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class PointEnum : IEnumerator<Point>
{
    private readonly List<Point> _points;
    private int i = -1;

    public PointEnum(List<Point> points)
    {
        this._points = points;
    }

    public bool MoveNext()
    {
        i++;
        return i < _points.Count;
    }

    public void Reset()
    {
        i = -1;
    }

    public Point Current
    {
        get
        {
            try
            {
                return _points[i];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        
    }
}