using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TSEvent : TSBehavior
{
    public GameObject missile;
    public Vector3 missileStartPos;
    public Vector3 missileTargetPos;
    public float missileSpeed;
    public Vector3 startPos;
    public GameObject failWindow;

    public TSEventGazeTarget[] gazeEvent;

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;
        if(IsClear())
            return;

        GameObject missileObj = Instantiate(missile) as GameObject;
        TSEventMissile tsMissile = missileObj.GetComponent<TSEventMissile>();

        tsMissile.Action( missileStartPos, missileTargetPos, missileSpeed); 
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

    public void Fail()
    {
        failWindow.SetActive(true);
    }

    public void Reset()
    {
        failWindow.SetActive(false);
        game.scene.state = SceneState.Play;
        game.scene.player.transform.position = startPos;
    }
}
