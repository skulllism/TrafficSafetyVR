using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using SWS;

public class CarCheckBox : MonoBehaviour
{

    private GameObject myCar;
    private splineMove mySplineMove;

    private float originSpeed;
    private float currentSpeed;

    public float cycle = 0.1f;
    public float degree = 1.5f;
    
    void Start()
    {
        myCar = transform.parent.gameObject;
        mySplineMove = myCar.GetComponent<splineMove>();
        currentSpeed = originSpeed = mySplineMove.speed;
    }

    void OnTriggerEnter(Collider enterColl)
    {
        if (enterColl.CompareTag("Car"))
        {
            StartCoroutine(Decelerate());
        }

        else if (enterColl.CompareTag("Crosswalk"))
        {
            Crosswalk ownTrafficLightTest = enterColl.gameObject.GetComponent<Crosswalk>();
            if (ownTrafficLightTest.trCar.currentSign == SignType.Red)
            {
                // mySplineMove.Pause();
                StartCoroutine(Decelerate());
            }
        }
    }

    void OnTriggerStay(Collider stayrColl)
    {
        if (stayrColl.CompareTag("Crosswalk"))
        {
            Crosswalk ownTrafficLightTest = stayrColl.gameObject.GetComponent<Crosswalk>();
            if (ownTrafficLightTest.trCar.currentSign == SignType.Green)
            {
                StartCoroutine(Accelerate());
            }
        }
    }

    void OnTriggerExit(Collider exitColl)
    {
        if (exitColl.CompareTag("Car"))
        {
            Invoke("Resume", Random.Range(0.5f, 1f));
        }
    }

    void Resume()
    {
        StartCoroutine(Accelerate());
        // mySplineMove.Resume();
    }

    IEnumerator Decelerate()
    {
        while (true)
        {
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
                mySplineMove.ChangeSpeed(currentSpeed);
                break;
            }
            currentSpeed -= degree;
            mySplineMove.ChangeSpeed(currentSpeed);
            yield return new WaitForSeconds(cycle);
        }
    }

    IEnumerator Accelerate()
    {
        while (true)
        {
            if (currentSpeed > originSpeed)
            {
                currentSpeed = originSpeed;
                mySplineMove.ChangeSpeed(currentSpeed);
                break;
            }
            currentSpeed += degree;
            mySplineMove.ChangeSpeed(currentSpeed);
            yield return new WaitForSeconds(cycle);
        }
    }
}
