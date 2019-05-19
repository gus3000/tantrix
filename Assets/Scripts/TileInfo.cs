using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TileInfo
{
    public static readonly Color[] ColorFromInt = new Color[]
        {Color.yellow, Color.red, Color.blue, Color.green, Color.white};

    public enum LineColor
    {
        YELLOW,
        RED,
        BLUE,
        GREEN,
        WHITE
    }

    public static LineColor[][] GetTilesFromText(string text)
    {
        string[] lines = text.Split('\n');
        LineColor[][] tiles = new LineColor[lines.Length - 1][];
        for (int i = 1; i < lines.Length; ++i)
        {
            tiles[i - 1] = GetTileFromLine(lines[i]);
        }

        return tiles;
    }

    public static LineColor[] GetTileFromLine(string line)
    {
        line = line.Trim();
        LineColor[] lineColors = new LineColor[6];
        LineColor lc;
        string[] info = line.Split(',');
        for (int i = 2; i < 8; ++i)
        {
            switch (info[i])
            {
                case "y":
                    lc = LineColor.YELLOW;
                    break;
                case "r":
                    lc = LineColor.RED;
                    break;
                case "b":
                    lc = LineColor.BLUE;
                    break;
                case "g":
                    lc = LineColor.GREEN;
                    break;
                case "w":
                    lc = LineColor.WHITE;
                    break;
                default:
                    lc = LineColor.WHITE;
                    break;
            }

            lineColors[i - 2] = lc;
        }

        return lineColors;
    }

    public static LineColor[] GetRotation(LineColor[] tile, int rotation)
    {
        LineColor[] newTile = new LineColor[6];
        for (int i = 0; i < 6; ++i)
            newTile[i] = tile[(i + rotation) % 6];
        return newTile;
    }
}