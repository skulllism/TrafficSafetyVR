using UnityEngine;
using System.Collections;
using  System.Collections.Generic;
using HutongGames.PlayMaker.Actions;

public enum SceneState
{
    Loading,
    Title,
    Play,
    Clear
}

public class Scene : FSMBase
{
    public Player player { private set; get; }
    private List<Actor> actors = new List<Actor>();

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType(typeof(Player)) as Player;
        game.SetScene(this);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Vector2.zero, new Vector2(100.0f, 100.0f)), "Title"))
            state = SceneState.Title;
        if (GUI.Button(new Rect(Vector2.right * 100.0f, new Vector2(100.0f, 100.0f)), "Play"))
            state = SceneState.Play;
        if (GUI.Button(new Rect(Vector2.right * 200.0f, new Vector2(100.0f, 100.0f)), "Clear"))
            state = SceneState.Clear;
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
        yield break;
    }
    #endregion

    #region Title

    private IEnumerator TitleEnterState()
    {
        game.ui.ActiveTitleWindow();
        yield break;
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

    #region Clear

    private IEnumerator ClearEnterState()
    {
        game.ui.ActiveClearWindow();
        yield break;
    }

    #endregion
}
