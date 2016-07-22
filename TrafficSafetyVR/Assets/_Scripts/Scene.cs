using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;

public enum SceneState
{
    Loading,
    Ready,
    Play,
    Clear,
    Fail
}

public class Scene : FSMBase
{
    public Player player { private set; get; }

    private Vehicle[] vehicles;
    private List<TrafficLight> trafficLights = new List<TrafficLight>();

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType(typeof(Player)) as Player;
        vehicles = FindObjectsOfType<Vehicle>();
        game.SetScene(this);
    }

    private void Start()
    {
        state = SceneState.Loading;
    }

    public void AddTrafficLight(TrafficLight trafficLight)
    {
        if (trafficLights.Contains(trafficLight))
            return;

        trafficLights.Add(trafficLight);
    }

    public void DeleteTrafficLight(TrafficLight trafficLight)
    {
        if (!trafficLights.Contains(trafficLight))
            return;

        trafficLights.Remove(trafficLight);
    }

    public void VehicleReset()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].sm.Stop();
        }
    }

    public void VehiclePause()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].sm.Pause();
        }
    }

    #region Loading

    private IEnumerator LoadingEnterState()
    {
        game.traffic.Init();
        state = SceneState.Ready;
        yield break;
    }
    #endregion

    #region Ready

    private IEnumerator ReadyEnterState()
    {
        game.ui.ActiveCtrlManualWindow();
        yield break;
    }

    private void ReadyUpdate()
    {
        game.ui.ManualUpdate();
        input.ManualUpdate();
        cam.ManualUpdate();
    }

    #endregion

    #region Play

    private IEnumerator PlayEnterState()
    {
        game.ui.ActivePlayWindow();

        for(int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].StartMove();
        }

        yield break;
    }

    private void PlayUpdate()
    {
        input.ManualUpdate();
        cam.ManualUpdate();
        player.ManualUpdate();
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].ManualUpdate();
        }
        for (int i = 0; i < trafficLights.Count; i++)
        {
            trafficLights[i].ManualUpdate();
        }
    }

    #endregion

    #region Fail

    private IEnumerator FailEnterState()
    {
        game.ui.ActiveFailWindow();
        VehiclePause();
        yield break;
    }

    private void FailUpdate()
    {
        game.ui.ManualUpdate();
        input.ManualUpdate();
        cam.ManualUpdate();
    }

    private IEnumerator FailExitState()
    {
        VehicleReset();
        yield break;
    }

    #endregion

    #region Clear

    private IEnumerator ClearEnterState()
    {
        game.ui.ActiveClearWindow();
        yield break;
    }

    #endregion
}
