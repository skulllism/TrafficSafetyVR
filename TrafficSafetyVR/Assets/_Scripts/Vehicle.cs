using UnityEngine;
using System.Collections;
using SWS;

[RequireComponent(typeof(BoxCollider))]
public class Vehicle : TSBehavior
{
    public float startDelay;

    public  splineMove sm { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        sm = GetComponent<splineMove>();
    }

    public void StartMove()
    {
        StartCoroutine(WaitForSecondAndGo(startDelay , ()=> {sm.StartMove();}));
    }

    public void GoToTarget(Vector3 point , float speed)
    {
        //TODO : Go To Target
    }

    public void GoToPlayer()
    {
        GoToTarget(game.scene.player.transform.position , 10);
    }

    private IEnumerator WaitForSecondAndGo(float waitTime , System.Action onComplete)
    {
        yield return new WaitForSeconds(waitTime);

        onComplete();
    }

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        //TODO : Go to fail
    }
}
