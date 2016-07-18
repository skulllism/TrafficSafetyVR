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
    private TSEvent[] events;

    private TSEvent failEvent = null;

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

    private void OnGUI()
    {
//        if (GUI.Button(new Rect(Vector2.zero, new Vector2(100.0f, 100.0f)), "Ready"))
//            state = SceneState.Ready;
//        if (GUI.Button(new Rect(Vector2.right * 100.0f, new Vector2(100.0f, 100.0f)), "Play"))
//            state = SceneState.Play;
//        if (GUI.Button(new Rect(Vector2.right * 200.0f, new Vector2(100.0f, 100.0f)), "Clear"))
//            state = SceneState.Clear;
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
    }

    #endregion

    #region Fail

    private IEnumerator FailEnterState()
    {
        for (int i = 0; i < events.Length; i++)
        {
            if(events[i].IsClear())
                continue;

            failEvent = events[i];
            failEvent.Fail();
        }
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
        failEvent.Reset();
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
