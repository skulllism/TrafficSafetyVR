using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(Crosswalk))]
public class EditorCrosswalk : Editor
{
    private Crosswalk crosswalk;

    private void OnEnable()
    {
        crosswalk = target as Crosswalk;
    }
    private void OnSceneGUI()
    {
        Handles.color = Color.red;

        Vector3 newPos = Handles.PositionHandle(crosswalk.eventStartPos, Quaternion.identity);
        if (newPos != crosswalk.eventStartPos)
        {
            Undo.RecordObject(crosswalk, "Move");
            crosswalk.eventStartPos = newPos;
        }

        Handles.SphereCap(0 , crosswalk.eventStartPos , Quaternion.identity,  1.0f);
    }
}
