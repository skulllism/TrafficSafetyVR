using UnityEngine;
using System.Collections;

public class TrafficLightPedestrian : TrafficLight
{
    private TSEventGazeTarget[] gazeEvents;

    protected override void Awake()
    {
        base.Awake();
        gazeEvents = GetComponentsInChildren<TSEventGazeTarget>();
    }

    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Pedestrian sign changed : " + type);

        if (type == SignType.Green)
        {
            for (int i = 0; i < gazeEvents.Length; i++)
            {
                gazeEvents[i].Reset();
            }
        }
        if (type == SignType.Red)
        {
            for (int i = 0; i < gazeEvents.Length; i++)
            {
                gazeEvents[i].GazeButtonDisable();
            }
        }
    }
}
