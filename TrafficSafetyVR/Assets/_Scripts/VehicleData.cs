using UnityEngine;
using System.Collections;

public enum VehicleType
{
    Red = 0,
    Blue,
    Max
}

public class VehicleData 
{
    public VehicleType type { set; get; }
    public string resourcePath { set; get; }
    public int index { set; get; }
    public float speed { set; get; }
    public float colliderZpos { set; get; }
}
