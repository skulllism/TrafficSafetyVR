using UnityEngine;
using System.Collections;

public class TrafficLightPedestrian : TrafficLight
{
    public GameObject missile;
    public Vector3 missileStartPos;
    public Vector3 missileTargetPos;
    public float missileSpeed;

    public Vector3 startPos;

    public GameObject failWindow;
    public Vector3 uiOffset;
    public float uiRotY;
    public TSGazeEvent gazeEvent { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        gazeEvent = GetComponentInChildren<TSGazeEvent>();
    }

    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Pedestrian sign changed : " + type);

        if (type == SignType.Green)
        {
            if (gazeEvent == null)
                return;

            gazeEvent.Reset();
            gazeEvent.gameObject.SetActive(true);
        }
        if (type == SignType.Red)
        {
            if(gazeEvent == null)
                return;

            gazeEvent.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        game.scene.state = SceneState.Play;
        game.scene.player.transform.position = startPos;
    }

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        if(currentSign == SignType.Green)
            return;

        GameObject missileObj = Instantiate(missile) as GameObject;
        TSEventMissile tsMissile = missileObj.GetComponent<TSEventMissile>();

        tsMissile.Action(failWindow, uiOffset, uiRotY, missileStartPos, missileTargetPos, missileSpeed);
    }
}
