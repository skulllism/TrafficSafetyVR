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

    private GameObject failWindow = null;

    protected override void Awake()
    {
        base.Awake();
        game.SetUI(this);
    }

    public void SetFailWindow(GameObject faillWindow)
    {
        this.failWindow = faillWindow;
    }

    public void Rotate(Vector3 targetRot)
    {
        Quaternion newRot = Quaternion.Euler(targetRot);
        transform.rotation = newRot;
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        transform.position = Vector3.Slerp(transform.position, game.scene.player.transform.position + baseOffSet, Time.deltaTime*speed);
    }

    public void ActiveFailWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);

        failWindow.SetActive(true);
    }

    public void ActiveCtrlManualWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(false);

        if(failWindow)
            failWindow.SetActive(false);

        ctrlManualWindow.SetActive(true);
    }

    public void ActiveObjectExplainWindow()
    {
        playWindow.SetActive(false);
        clearWindow.SetActive(false);
        if (failWindow)
            failWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);

        objectExplainWindow.SetActive(true);
    }

    public void ActivePlayWindow()
    {
        clearWindow.SetActive(false);
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);

        if (failWindow)
            failWindow.SetActive(false);

        playWindow.SetActive(true);
    }

    public void ActiveClearWindow()
    {
        objectExplainWindow.SetActive(false);
        ctrlManualWindow.SetActive(false);
        playWindow.SetActive(false);

        if (failWindow)
            failWindow.SetActive(false);

        clearWindow.SetActive(true);
    }

    public void GoToPlayScene()
    {
        game.scene.state = SceneState.Play;
    }
}
