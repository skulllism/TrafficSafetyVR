using UnityEngine;
using System.Collections;

public class TrafficLightPedestrian : TrafficLight
{
    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Pedestrian sign changed : " + type);

        if (type == SignType.Green)
        {
        }
        if (type == SignType.Red)
        {
        }
    }
}
