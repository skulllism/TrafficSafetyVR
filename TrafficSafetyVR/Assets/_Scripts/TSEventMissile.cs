using UnityEngine;
using System.Collections;

public class TSEventMissile : Actor
{
    private Vector3 uiOffset;
    private float uiRotY;
    private GameObject failWindow;
    private Vector3 resetPos;

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        //TODO : Go to fail
    }


    public void Action(GameObject failWindow  , Vector3 uiOffset , float uiRotY, Vector3 startPos, Vector3 targetPos , float speed , Vector3 resetPos)
    {
        this.resetPos = resetPos;
        this.failWindow = failWindow;
        this.uiOffset = uiOffset;
        this.uiRotY = uiRotY;
        transform.position = startPos;
        MakeDirection(targetPos);
        this.speed = speed;
        SetAccel(1.0f);
        transform.LookAt(transform.localPosition + direction);

//        game.scene.AddActor(this);
    }
}
