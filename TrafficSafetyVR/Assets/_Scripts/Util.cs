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
    public static bool IsPlatformAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    public static bool IsPlatformIOS()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }

    public static bool IsMobilePlatform()
    {
        if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            return true;
        }
        return false;
    }
    public static int Int32ParseFast(string value)
    {
        int result = 0;
        int length = value.Length;
        bool minus = false;
        for (int i = 0; i < length; i++)
        {
            if (value[i].CompareTo('-') == 0)
            {
                minus = true;
                continue;
            }
            result = 10 * result + (value[i] - 48);
        }
        return minus == false ? result : -result;
    }
}
