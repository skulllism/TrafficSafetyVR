using UnityEngine;
using System.Collections;

public class TSEvent : TSBehavior
{
    public GameObject missile;
    public Vector3 missileStartPos;
    public Vector3 missileTargetPos;
    public float missileSpeed;
    public Vector3 startPos;
    public GameObject failWindow;

    private bool clear = false;

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        GameObject missileObj = Instantiate(missile) as GameObject;
        TSEventMissile tsMissile = missileObj.GetComponent<TSEventMissile>();

        tsMissile.Action( missileStartPos, missileTargetPos, missileSpeed); 
    }

    public void Clear()
    {
        clear = true;
        Debug.Log("Clear");
    }

    public bool IsClear()
    {
        return clear;
    }

    public void Fail()
    {
        Debug.Log("Fali");
    }

    public void Reset()
    {
        
    }
}
