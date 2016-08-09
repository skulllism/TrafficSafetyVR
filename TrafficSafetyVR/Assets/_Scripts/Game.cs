using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game
{
    private static Game instance = null;
    

    public static Game Instance
    {
        get
        {
            if(instance == null)
                instance = new Game();

            return instance;
        }
    }

    public Scene scene { private set; get; }
    public UIActionManager ui { private set; get; }
    public Traffic traffic { private set; get; }
    public Platform platform { private set; get; }
    public DataContainer container { private set; get; }

    public Game()
    {
        container = new DataContainer();

        if (Util.IsEditorPlatform())
        {
            platform = new DesktopPlatform();
        }
        else if (Util.IsPlatformAndroid())
        {
            platform = new AndroidPlatform();
        }
        else if (Util.IsPlatformIOS())
            platform = new iOSPlatform();
    }

    public void SetTraffic(Traffic traffic)
    {
        this.traffic = traffic;
    }

    public void SetScene(Scene scene)
    {
        this.scene = scene;
    }
    
    public void SetUI(UIActionManager ui)
    {
        this.ui = ui;
    }
}
