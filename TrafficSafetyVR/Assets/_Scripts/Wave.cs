using UnityEngine;
using System.Collections;
using SWS;

public class Wave : MonoBehaviour
{

    public GameObject[] carObject;
    public float[] startDelay;

	void Start () {
        for (int i = 0; i < carObject.Length; i++)
	    {
	        splineMove sM = carObject[i].GetComponent<splineMove>();
            sM.StartMove();
            if(startDelay[i] > 0)
                sM.Pause(startDelay[i]);
	    }
	}
	
	void Update () {
	
	}
}
