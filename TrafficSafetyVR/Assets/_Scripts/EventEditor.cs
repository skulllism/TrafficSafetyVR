using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(TSEvent))]
public class EventEditor : Editor
{
    private TSEvent tsEvent;
    private void OnEnable()
    {
        tsEvent = target as TSEvent;
    }
    private void OnSceneGUI()
    {
        Handling(ref tsEvent.missileStartPos,"Missile Start" , Color.red );
        Handling(ref tsEvent.missileTargetPos, "Missile Target", Color.blue);
        Handling(ref tsEvent.startPos, "Event Start", Color.green);
    }

    private void Handling(ref Vector3 target, string label, Color capColor)
    {
        Handles.color = capColor;
        Vector3 newPos = Handles.PositionHandle(target, Quaternion.identity);
        if (newPos != target)
        {
            Undo.RecordObject(tsEvent, "Move");

            target = newPos;
        }
        Handles.SphereCap(0, target, Quaternion.identity, 0.5f);
        Handles.Label(target + new Vector3(0f, 3f, 0f), label);
    }
}