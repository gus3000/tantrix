using UnityEngine;

public static class HexMetrics
{
    // on a besoin de deux rayons : celui des coins (l' "extérieur") et celui de la médiane (le plus proche)
    public const float OuterRadius = 1f;
    public const float InnerRadius = OuterRadius * .866025404f; // outerRadius * sqrt(3) / 2

    public static Vector3[] corners =
    {
        //centre gauche, puis clockwise
        new Vector3(-OuterRadius, 0, 0),
        new Vector3(-.5f * OuterRadius, 0, InnerRadius),
        new Vector3(.5f * OuterRadius, 0, InnerRadius),
        new Vector3(OuterRadius, 0, 0),
        new Vector3(.5f * OuterRadius, 0, -InnerRadius),
        new Vector3(-.5f * OuterRadius, 0, -InnerRadius),
    };

    public static Vector3[] sides =
    {
        Vector3.Lerp(corners[0], corners[1], 0.5f),
        Vector3.Lerp(corners[1], corners[2], 0.5f),
        Vector3.Lerp(corners[2], corners[3], 0.5f),
        Vector3.Lerp(corners[3], corners[4], 0.5f),
        Vector3.Lerp(corners[4], corners[5], 0.5f),
        Vector3.Lerp(corners[5], corners[0], 0.5f),
    };

    public static Vector3 CellCoords(int x, int y)
    {
        Vector3 position = Vector3.zero;
        
        position.x = x * HexMetrics.OuterRadius * 1.5f;
        position.z = (y + x * .5f /*- Mathf.Floor(x / 2f)*/) * HexMetrics.InnerRadius * 2;
        return position;
    }
}