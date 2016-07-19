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
    public TSEvent failEvent { private set; get; }

    private List<Actor> actors = new List<Actor>();
    private TSEvent[] events;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType(typeof(Player)) as Player;
        game.SetScene(this);

        events = FindObjectsOfType<TSEvent>();
    }

    private void Start()
    {
        state = SceneState.Loading;
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

    public void SetFailEvent(TSEvent failEvent)
    {
        this.failEvent = failEvent;
    }

    private void ActiveEvent()
    {
        for (int i = 0; i < events.Length; i++)
        {
            if (events[i].IsClear())
            {
                events[i].gameObject.SetActive(false);
                continue;
            }

            events[i].gameObject.SetActive(true);
        }
    }

    private void DisableEvent()
    {
        for (int i = 0; i < events.Length; i++)
        {
            events[i].gameObject.SetActive(false);
        }
    }

    #region Loading

    private IEnumerator LoadingEnterState()
    {
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
    }

    #endregion

    #region Fail

    private IEnumerator FailEnterState()
    {
        failEvent.Fail();
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
        DeleteAll();
        failEvent = null;
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
