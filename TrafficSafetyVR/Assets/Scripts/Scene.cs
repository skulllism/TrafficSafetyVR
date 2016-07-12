using UnityEngine;
using System.Collections;

public enum SceneState
{
    Loading,
    Title,
    Play,
    Clear
}

public class Scene : FSMBase
{
    protected override void Awake()
    {
        base.Awake();
        game.SetScene(this);
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
        yield break;
    }

    #endregion

    #region Play

    private IEnumerator PlayEnterState()
    {
        yield break;
    }

    private void PlayManualUpdate()
    {

    }

    #endregion
}
