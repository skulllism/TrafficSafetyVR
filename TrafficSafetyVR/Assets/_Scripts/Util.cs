using UnityEngine;
using System.Collections;

public class Util 
{
    public static bool IsEditorPlatform()
    {
#if UNITY_EDITOR
        return true;
#else
            return false;
#endif
    }
}
