using System;
using System.IO.Compression;
using UnityEngine;

[Serializable]
public class HexCoordinates
{
    public int X { get; private set; }

    public int Y { get; private set; }
//    public int Z { get; private set; }

    public HexCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int y)
    {
        return new HexCoordinates(x, y - x / 2);
    }

    public HexCoordinates[] Neighbours()
    {
        HexCoordinates[] neighbours = new HexCoordinates[6];

        neighbours[0] = new HexCoordinates(X, Y + 1);
        neighbours[1] = new HexCoordinates(X+1, Y);
        neighbours[2] = new HexCoordinates(X+1, Y-1);
        neighbours[3] = new HexCoordinates(X, Y - 1);
        neighbours[4] = new HexCoordinates(X-1, Y);
        neighbours[5] = new HexCoordinates(X-1, Y+1);

        return neighbours;
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ")";
    }

    public override bool Equals(object obj)
    {
        if (! (obj is HexCoordinates))
            return false;
        var other = obj as HexCoordinates;
        return this.X == other.X && this.Y == other.Y;
    }
}