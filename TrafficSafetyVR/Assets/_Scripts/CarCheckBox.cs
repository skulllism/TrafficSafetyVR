using UnityEngine;
using System.Collections;
using SWS;

public class CarCheckBox : MonoBehaviour
{

    private GameObject myCar;
    private CarTest myCarTest;
    private splineMove mySplineMove;

    void Start()
    {
        myCar = transform.parent.gameObject;
        myCarTest = myCar.GetComponent<CarTest>();
        mySplineMove = myCar.GetComponent<splineMove>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            mySplineMove.Pause();
        }

        else if (other.CompareTag("Crosswalk"))
        {
            TrafficLightTest ownTrafficLightTest = other.gameObject.GetComponent<TrafficLightTest>();
            if (ownTrafficLightTest.redLight == true)
            {
                mySplineMove.Pause();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Crosswalk"))
        {
            TrafficLightTest ownTrafficLightTest = other.gameObject.GetComponent<TrafficLightTest>();
            if (ownTrafficLightTest.redLight == false)
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
