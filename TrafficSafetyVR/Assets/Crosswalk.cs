using UnityEngine;
using System.Collections;

public class Crosswalk : TSBehavior
{
    public GameObject failNotGazeWindow;
    
    public GameObject failJaywalkingWindow;

    public Vector3 failPos;
    public float failRotY;

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
            Accident(failJaywalkingWindow , failPos, failRotY);
            return;
        }

        if (trPedestrian.currentSign == SignType.Green)
        {
            return;

            //TODO : Check gaze event clear
            Accident(failNotGazeWindow, failPos, failRotY);
            return;
        }


    }

    private void Accident(GameObject failWindow, Vector3 uiPos , float uiRotY)
    {
        game.ui.SetFailWindow(failWindow);
        game.ui.Rotate(new Vector3(0.0f , uiRotY, 0.0f));
        game.ui.baseOffSet = uiPos;
        print("dd");
        game.scene.state = SceneState.Fail;
    }
}
