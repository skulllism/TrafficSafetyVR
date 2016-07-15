using UnityEngine;
using System.Collections;

public enum PlayerState
{
    Idle,
    Move
}

public class PlayerFSM : FSMBase
{
    private Player player;
    private Vector3 savePos;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    #region Idle

    private IEnumerator IdleEnterState()
    {
        Debug.Log("Idle");
        cam.state = PlayerState.Idle;
        yield break;
    }

    private void IdleManualUpdate()
    {
    }

    private IEnumerator IdleExitState()
    {
        yield break;
    }

    #endregion

}

