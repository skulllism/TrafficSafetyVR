﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using GazeInput;
using HutongGames.PlayMaker.Actions;
>>>>>>> origin/master
using UnityEngine.VR;
using DG.Tweening;
using UnityEngine.UI;

public enum SceneState
{
    Loading,
    Ready,
    Play,
    Clear,
    Fail,
    Accident,
}

public class Scene : FSMBase
{
    public Player player { private set; get; }

    private Vehicle[] vehicles;
    private List<TrafficLight> trafficLights = new List<TrafficLight>();
    private Vehicle missile;

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

    public void AddMissile(Vehicle vehicle)
    {
        this.missile = vehicle;
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

    public void VehicleStartMove()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].sm.StartMove();
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
        airVRCamRig.GetComponent<VREventSystem>().autoClickTime = 2.0f;
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
        airVRCamRig.GetComponent<VREventSystem>().autoClickTime = 0.01f;
        game.ui.ActivePlayWindow();
        VehicleStartMove();

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

    public Vector3 failCamPos;

    private IEnumerator FailEnterState()
    {
        cam.transform.position = player.transform.position+ failCamPos;
        cam.transform.LookAt(game.scene.player.transform.position);
        InputTracking.Recenter();
        game.ui.Rotate(cam.transform.rotation.eulerAngles);
        game.ui.transform.position = cam.transform.position + cam.transform.forward * 2.0f;
        game.ui.ActiveFailWindow();
        cam.Reset();
        yield break;
    }

    private void FailUpdate()
    {
        input.ManualUpdate();
    }

    private IEnumerator FailExitState()
    {
        VehicleReset();
        Destroy(missile.gameObject);
        missile = null;
        yield break;
    }

    #endregion

    #region Accident

    public float actionTime = 0.1f;
    public Image fadeImage;
    private float nowTime = 0.0f;

    private IEnumerator AccidentEnterState()
    {
        VehiclePause();
        print("ghewfewfew");
        fadeImage.DOFade(0.5f, actionTime);
        yield break;
    }

    private void AccidentUpdate()
    {
        input.ManualUpdate();
        cam.ManualUpdate();
        missile.ManualUpdate();

        // Debug.Log(nowTime);
        if (nowTime < actionTime)
        {
            nowTime += Time.deltaTime;
            return;
        }

        ChangeStateToFail();
    }

    private void ChangeStateToFail()
    {
        state = SceneState.Fail;
    }

    private IEnumerator AccidentExitState()
    {
        nowTime = 0.0f;
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
