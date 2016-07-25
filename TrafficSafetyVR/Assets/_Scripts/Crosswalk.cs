using UnityEngine;
using System.Collections;

public class Crosswalk : TSBehavior
{
    public GameObject failNotGazeWindow;
    
    public GameObject failJaywalkingWindow;

    public Vector3 failPos;

    public TrafficLightCar trCar { private set; get; }
    public TrafficLightPedestrian trPedestrian { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        trCar = GetComponentInChildren<TrafficLightCar>();
        trPedestrian = GetComponentInChildren<TrafficLightPedestrian>();
    }

    private void OnTriggerStay(Collider stayColl)
    {
        if(game.scene.state.ToString() != SceneState.Play.ToString())
            return;

        if(!stayColl.CompareTag("Player"))
            return;

        if (trPedestrian.currentSign == SignType.Red)
        {
            Accident(failJaywalkingWindow , failPos);
            return;
        }

        if (trPedestrian.currentSign == SignType.Green)
        {
            return;

            //TODO : Check gaze event clear
            Accident(failNotGazeWindow, failPos);
            return;
        }


    }

    private void Accident(GameObject failWindow, Vector3 uiPos)
    {
        game.ui.SetFailWindow(failWindow);
        game.ui.Rotate(game.scene.player.transform.rotation.eulerAngles);
        game.ui.transform.position = game.scene.player.transform.position + game.scene.player.transform.forward*uiPos.z +
                             new Vector3(0.0f, uiPos.y, 0.0f);
        game.scene.state = SceneState.Fail;
    }
}
