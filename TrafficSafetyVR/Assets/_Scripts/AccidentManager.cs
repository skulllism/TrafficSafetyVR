using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class AccidentManager : TSBehavior
{
    public GameObject missile;
    public Vector3 missailCreatePos;
    public bool InRange { private set; get; }

    private Crosswalk crosswalk;

    protected override void Awake()
    {
        base.Awake();
        crosswalk = GetComponentInParent<Crosswalk>();
    }

    public void Accident()
    {
        Vehicle newMissile = GameObject.Instantiate(missile).GetComponent<Vehicle>();
        newMissile.transform.position = transform.position + missailCreatePos ;
        newMissile.GoToPlayer();
        cam.SetAccidentMode();
        game.scene.state = SceneState.Accident;
    }

    private void OnTriggerStay(Collider stayColl)
    {
        if (!stayColl.CompareTag("Player"))
        {
            InRange = false;
            return;
        }

        InRange = true;
    }
}
