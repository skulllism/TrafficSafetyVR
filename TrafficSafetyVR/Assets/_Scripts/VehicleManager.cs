using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour
{

    public static VehicleManager instance;
    public GameObject[] vehicles;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
