using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(TSGazeEvent))]
public class EventEditor : Editor
{
    private TSGazeEvent _tsGazeEvent;
    private void OnEnable()
    {
        _tsGazeEvent = target as TSGazeEvent;
    }
    private void OnSceneGUI()
    {
        Handling(ref _tsGazeEvent.missileStartPos,"Missile Start" , Color.red );
        Handling(ref _tsGazeEvent.missileTargetPos, "Missile Target", Color.blue);
        Handling(ref _tsGazeEvent.startPos, "Event Start", Color.green);
    }

    private void Handling(ref Vector3 target, string label, Color capColor)
    {
        Handles.color = capColor;
        Vector3 newPos = Handles.PositionHandle(target, Quaternion.identity);
        if (newPos != target)
        {
            Undo.RecordObject(_tsGazeEvent, "Move");

            target = newPos;
        }
        Handles.SphereCap(0, target, Quaternion.identity, 0.5f);
        Handles.Label(target + new Vector3(0f, 3f, 0f), label);
    }
}