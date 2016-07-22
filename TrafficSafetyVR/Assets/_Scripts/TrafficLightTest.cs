using UnityEngine;
using System.Collections;

public class TrafficLightTest : MonoBehaviour
{

    public static TrafficLightTest instance;

    public bool redLight;
    public bool yellowLight;
    public bool greenLight;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
