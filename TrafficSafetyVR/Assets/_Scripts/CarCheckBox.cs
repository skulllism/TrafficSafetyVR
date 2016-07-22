using UnityEngine;
using System.Collections;
using SWS;

public class CarCheckBox : MonoBehaviour
{

    private GameObject myCar;
    private splineMove mySplineMove;

    void Start()
    {
        myCar = transform.parent.gameObject;
        mySplineMove = myCar.GetComponent<splineMove>();
    }

    void OnTriggerEnter(Collider enterColl)
    {
        if (enterColl.CompareTag("Car"))
        {
            mySplineMove.Pause();
        }

        else if (enterColl.CompareTag("Crosswalk"))
        {
            Crosswalk ownTrafficLightTest = enterColl.gameObject.GetComponent<Crosswalk>();
            if (ownTrafficLightTest.trCar.currentSign == SignType.Red)
            {
                mySplineMove.Pause();
            }
        }
    }

    void OnTriggerStay(Collider enterColl)
    {

        if (enterColl.CompareTag("Crosswalk"))
        {
            Crosswalk ownTrafficLightTest = enterColl.gameObject.GetComponent<Crosswalk>();
            if (ownTrafficLightTest.trCar.currentSign == SignType.Green)
            {
                mySplineMove.Resume();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Invoke("Resume", Random.Range(0.5f, 1f));
        }
    }

    void Resume()
    {
        mySplineMove.Resume();
    }
}
