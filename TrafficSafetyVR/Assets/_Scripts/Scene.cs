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
    private TSGazeEvent[] _gazeEvents;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType(typeof(Player)) as Player;
        game.SetScene(this);

        _gazeEvents = FindObjectsOfType<TSGazeEvent>();
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

    public void GoToFail(GameObject failWindow , Vector3 uiOffset , float rotY)
    {
        this.failWindow = failWindow;
        game.ui.baseOffSet = uiOffset;
        game.ui.Rotate(new Vector3(0.0f, rotY, 0.0f));
        failWindow.SetActive(true);
        state = SceneState.Fail;
    }

    private void ActiveEvent()
    {
        for (int i = 0; i < _gazeEvents.Length; i++)
        {
            if (_gazeEvents[i].IsClear())
            {
                _gazeEvents[i].gameObject.SetActive(false);
                continue;
            }

            _gazeEvents[i].gameObject.SetActive(true);
        }
    }

    private void DisableEvent()
    {
        for (int i = 0; i < _gazeEvents.Length; i++)
        {
            _gazeEvents[i].gameObject.SetActive(false);
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
        DisableEvent();
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
        ActiveEvent();
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

    private GameObject failWindow;

    private void FailUpdate()
    {
        game.ui.ManualUpdate();
        input.ManualUpdate();
        cam.ManualUpdate();
    }

    private IEnumerator FailExitState()
    {
        DeleteAll();
        failWindow.SetActive(false);
        failWindow = null;
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
