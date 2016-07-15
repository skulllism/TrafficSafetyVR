using UnityEngine;
using System.Collections;

public class UIActionManager : TSBehavior
{
    public GameObject titleWindow;

    public GameObject playWindow;

    public GameObject clearWindow;

    protected override void Awake()
    {
        base.Awake();
        game.SetUI(this);
    }

    public void ActiveTitleWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);

        titleWindow.SetActive(true);
    }

    public void ActivePlayWindow()
    {
        clearWindow.SetActive(false);
        titleWindow.SetActive(false);

        playWindow.SetActive(true);
    }

    public void ActiveClearWindow()
    {
        titleWindow.SetActive(false);
        playWindow.SetActive(false);

        clearWindow.SetActive(true);
    }
}
