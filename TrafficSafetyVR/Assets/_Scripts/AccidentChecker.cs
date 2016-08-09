using UnityEngine;
using System.Collections;

public class AccidentChecker : TSBehavior
{
    private Vehicle vehicle;

    protected override void Awake()
    {
        base.Awake();
        vehicle = GetComponentInParent<Vehicle>();
    }

    private void OnTriggerEnter(Collider enterColl)
    {
        if (!enterColl.CompareTag("Player"))
            return;
        vehicle.SetAccel(0.0f);
        game.scene.player.GetComponent<Rigidbody>().AddForce(vehicle.direction * 3000.0f);
    }
}
