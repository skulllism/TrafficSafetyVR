using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TSGazeEvent : TSBehavior
{
    public GameObject missile;
    public Vector3 missileStartPos;
    public Vector3 missileTargetPos;
    public GameObject failWindow;
    public Vector3 uiOffset;
    public float uiRotY;

    public float missileSpeed;
    public Vector3 startPos;

    public TSEventGazeTarget[] gazeEvent;

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;
        if(IsClear())
            return;

        GameObject missileObj = Instantiate(missile) as GameObject;
        TSEventMissile tsMissile = missileObj.GetComponent<TSEventMissile>();

        tsMissile.Action(failWindow ,uiOffset , uiRotY, missileStartPos, missileTargetPos, missileSpeed); 
    }

    public bool IsClear()
    {
        for (int i = 0; i < gazeEvent.Length; i++)
        {
            if (!gazeEvent[i].clear)
                return false;
        }

        return true;
    }

    public void Reset()
    {
        game.scene.state = SceneState.Play;
        game.scene.player.transform.position = startPos;
        for (int i = 0; i < gazeEvent.Length; i++)
        {
            gazeEvent[i].Reset();
        }
    }
}
