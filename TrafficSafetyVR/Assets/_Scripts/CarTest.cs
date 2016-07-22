using UnityEngine;
using System.Collections;
using SWS;

public class CarTest : MonoBehaviour
{

    private splineMove mySplineMove;

	// Use this for initialization
	void Start ()
	{
	    mySplineMove = GetComponent<splineMove>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void stop()
    {
        if (TrafficLightTest.instance.redLight == true)
        {
            mySplineMove.Pause(5);
        }
    }
}
