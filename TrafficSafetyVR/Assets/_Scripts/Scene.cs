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

    private List<Actor> actors = new List<Actor>();
    private List<TrafficLight> trafficLights = new List<TrafficLight>();

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType(typeof(Player)) as Player;
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

    public void AddActor(Actor actor)
    {
        if(actors.Contains(actor))
            return;

        actors.Add(actor);
    }

    public void DeleteActor(Actor actor)
    {
        if(!actors.Contains(actor))
            return;

        actors.Remove(actor);
        Destroy(actor.gameObject);
    }

    public void DeleteAll()
    {
        for (int i = 0; i < actors.Count; i++)
        {
            DeleteActor(actors[i]);
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
        yield break;
    }

    private void PlayUpdate()
    {
        input.ManualUpdate();
        cam.ManualUpdate();
        player.ManualUpdate();
        for (int i = 0; i < actors.Count; i++)
        {
            actors[i].ManualUpdate();
        }
        for (int i = 0; i < trafficLights.Count; i++)
        {
            trafficLights[i].ManualUpdate();
        }
    }

    #endregion

    #region Fail

    private void FailUpdate()
    {
        game.ui.ManualUpdate();
        input.ManualUpdate();
        cam.ManualUpdate();
    }

    private IEnumerator FailExitState()
    {
        DeleteAll();
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
