using UnityEngine;
using System.Collections;

public class TSEventMissile : Actor
{
    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        Fail();
    }

    private void Fail()
    {
        game.scene.state = SceneState.Fail;
    }

    public void Action(Vector3 startPos, Vector3 targetPos , float speed)
    {
        transform.position = startPos;
        MakeDirection(targetPos);
        this.speed = speed;
        SetAccel(1.0f);

        game.scene.AddActor(this);
    }
}
