using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public static class CurvesUtil
{
    public static Vector3 EvaluateQuadratic(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        Vector3 p0 = Vector3.Lerp(start, control, t);
        Vector3 p1 = Vector3.Lerp(control, end, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public static bool Intersect(int start1, int end1, int start2, int end2)
    {
        return Math.Abs(start1 - end1) != 1 && Math.Abs(start2 - end2) != 1;
    }
    
//    public static bool
}