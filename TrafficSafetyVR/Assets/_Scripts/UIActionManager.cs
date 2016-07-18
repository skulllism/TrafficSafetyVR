using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class UIActionManager : TSBehavior
{
    public Vector3 baseOffSet;

    public float speed;

    public GameObject ctrlManualWindow;

    public GameObject objectExplainWindow;

    public GameObject playWindow;

    public GameObject clearWindow;

    protected override void Awake()
    {
        base.Awake();
        game.SetUI(this);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        transform.position = Vector3.Slerp(transform.position, game.scene.player.transform.position + baseOffSet, Time.deltaTime*speed);
    }

    public void ActiveCtrlManualWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(true);
    }

    public void ActiveObjectExplainWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(true);
        ctrlManualWindow.SetActive(false);
    }

    public void ActivePlayWindow()
    {
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);

        playWindow.SetActive(true);
    }

    public void ActiveClearWindow()
    {
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);
        playWindow.SetActive(false);

        clearWindow.SetActive(true);
    }
}
