using System.IO;
using UnityEngine;

public abstract class Platform
{
    public abstract string GetStreamingAssetsPath(string filename);
}

public class DesktopPlatform : Platform
{
    public override string GetStreamingAssetsPath(string filename)
    {
        return Application.dataPath + "/StreamingAssets" + filename;
    }
}

public class iOSPlatform : Platform
{
    public override string GetStreamingAssetsPath(string filename)
    {
        return Application.dataPath + "/Raw" + filename;
    }
}

public class AndroidPlatform : Platform
{
    public override string GetStreamingAssetsPath(string filename)
    {
        string strFilePath = Application.persistentDataPath + filename;

        WWW wwwUrl = new WWW("jar:file://" + Application.dataPath + "!/assets" + filename);
        while (!wwwUrl.isDone) { }

        if (string.IsNullOrEmpty(wwwUrl.error) == false)
        {
            return "file not found";
        }

        File.WriteAllBytes(strFilePath, wwwUrl.bytes);

        return strFilePath;
    }
}