using UnityEngine;
using System.Collections;
using SWS;

[RequireComponent(typeof(BoxCollider))]
public class Vehicle : Actor
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

    public void GoToTarget(Vector3 point)
    {
        SetAccel(1.0f);
        MakeDirection(point);
        transform.LookAt(transform.position + direction);
        game.scene.AddMissile(this);
    }

    public void GoToPlayer()
    {
        GoToTarget(new Vector3(game.scene.player.transform.position.x , transform.position.y , game.scene.player.transform.position.z));
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
        SetAccel(0.0f);
        game.scene.player.GetComponent<Rigidbody>().AddForce(direction * 3000.0f);
        cam.SetAccidentMode();
    }   
}
