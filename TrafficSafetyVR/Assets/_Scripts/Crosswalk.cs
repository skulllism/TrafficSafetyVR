using UnityEngine;
using System.Collections;

public class Crosswalk : TSBehavior
{
    public GameObject failNotGazeWindow;
    
    public GameObject failJaywalkingWindow;

    public Vector3 eventStartPos;

    public AccidentManager[] accidentMgr { private set; get; }
    public TrafficLightCar trCar { private set; get; }
    public TrafficLightPedestrian trPedestrian { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        trCar = GetComponentInChildren<TrafficLightCar>();
        trPedestrian = GetComponentInChildren<TrafficLightPedestrian>();
        accidentMgr = GetComponentsInChildren<AccidentManager>();
    }

    private void OnTriggerStay(Collider stayColl)
    {
        if(game.scene.state.ToString() != SceneState.Play.ToString())
            return;

        if(!stayColl.CompareTag("Player"))
            return;

        if (trPedestrian.currentSign == SignType.Red)
        {
            for (int i = 0; i < accidentMgr.Length; i++)
            {
                if (accidentMgr[i].InRange)
                {
                    accidentMgr[i].Accident();
                    game.ui.SetFailWindow(failJaywalkingWindow);
                    break;
                }
            }
            return;
        }

        if (trPedestrian.currentSign == SignType.Green)
        {
            if (!trPedestrian.IsGazeEventClear())
            {
                for (int i = 0; i < accidentMgr.Length; i++)
                {
                    if (accidentMgr[i].InRange)
                    {
                        accidentMgr[i].Accident();
                        game.ui.SetFailWindow(failNotGazeWindow);
                        break;
                    }
                }
            }
        }
    }

    public void Reset()
    {
        game.scene.player.transform.position = eventStartPos;
        game.scene.state = SceneState.Play;
    }
}
