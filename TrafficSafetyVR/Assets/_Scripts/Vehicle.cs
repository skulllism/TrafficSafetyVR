using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BoxCollider))]
public class Vehicle : TSBehavior
{
    private NavMeshAgent navAgent;

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void GoToTarget(Vector3 point , float speed)
    {
        navAgent.speed = speed;
        navAgent.SetDestination(point);
    }

    public void GoToPlayer()
    {
        GoToTarget(game.scene.player.transform.position , 10);
    }

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        //TODO : Go to fail
    }
}
