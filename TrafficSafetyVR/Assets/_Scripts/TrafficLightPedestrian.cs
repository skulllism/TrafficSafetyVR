using UnityEngine;
using System.Collections;

public class TrafficLightPedestrian : TrafficLight
{
    private GazeEvent[] gazeEvents;

    protected override void Awake()
    {
        base.Awake();
        gazeEvents = GetComponentsInChildren<GazeEvent>();
    }

    public void ActiveGazeEvents()
    {
        for (int i = 0; i < gazeEvents.Length; i++)
        {
            gazeEvents[i].gameObject.SetActive(true);
        }
    }

    public void DisableGazeEvents()
    {
        for (int i = 0; i < gazeEvents.Length; i++)
        {
            gazeEvents[i].Reset();
            gazeEvents[i].gameObject.SetActive(false);
        }
    }

    public bool IsGazeEventsComplete()
    {
        for (int i = 0; i < gazeEvents.Length; i++)
        {
            if (!gazeEvents[i].complete)
                return false;
        }

        return true;
    }

    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Pedestrian sign changed : " + type);

        if (type == SignType.Green)
        {
            ActiveGazeEvents();
        }

        if (type == SignType.Red)
        {
            DisableGazeEvents();
        }
    }
}
