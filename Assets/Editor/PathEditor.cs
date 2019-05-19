﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    private PathCreator creator;
    private Path path;

    private void OnSceneGUI()
    {
        Input();
        Draw();
    }

    private void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add Point");
            path.AddPoint(mousePos);
        }
    }

    private void Draw()
    {
        Color oldColor = Handles.color;
        
        
        //LINES
        Handles.color = Color.white;
        for (int i = 0; i < path.NumSegments; i++)
            Handles.DrawBezier(path[i].Origin, path[i + 1].Origin, path[i].SecondaryHandle, path[i+1].Handle, Color.green, null, 2);

        Handles.color = Color.black;
        foreach (Point p in path)
            Handles.DrawLine(p.Origin, p.Handle);
        
        Handles.color = Color.grey;
        foreach (Point p in path)
        {
            Handles.DrawLine(p.Origin, p.SecondaryHandle);
            Handles.FreeMoveHandle(p.SecondaryHandle, Quaternion.identity, .05f, Vector2.zero, Handles.CylinderHandleCap);
        }


        //POINTS
        Handles.color = Color.red;
        foreach (Point p in path)
        {
            Vector2 newPos = Handles.FreeMoveHandle(p.Origin, Quaternion.identity, .1f, Vector2.zero,
                Handles.CylinderHandleCap);
            if (p.Origin != newPos)
            {
                Undo.RecordObject(creator, "Move Point");
                p.Move(newPos);
            }
        }

        Handles.color = Color.black;
        foreach (Point p in path)
        {
            Vector2 newPos = Handles.FreeMoveHandle(p.Handle, Quaternion.identity, .05f, Vector2.zero,
                Handles.CylinderHandleCap);
            if (p.Handle != newPos)
            {
                Undo.RecordObject(creator, "Move Handle");
                p.MoveHandle(newPos);
            }
        }

        Handles.color = oldColor;
    }

    private void OnEnable()
    {
        creator = (PathCreator) target;
        if (creator.path == null)
            creator.CreatePath();
        path = creator.path;
    }
}