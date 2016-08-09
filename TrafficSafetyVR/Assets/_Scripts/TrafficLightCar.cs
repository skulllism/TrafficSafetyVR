using UnityEngine;
using System.Collections;

public class TrafficLightCar : TrafficLight
{
    public float greenSignTime;
    public float redSignTime;
    public float yellowSignTime;

    private float nowTime = 0.0f;
    private TrafficLightPedestrian[] trafficPedestrians;

    protected override void Awake()
    {
        base.Awake();
        trafficPedestrians = GetComponentsInChildren<TrafficLightPedestrian>();
        
    }

    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Car sign changed : " + type);

        if (type == SignType.Green)
        {
            for (int i = 0; i < trafficPedestrians.Length; i++)
            {
                trafficPedestrians[i].SetSign(SignType.Red);
            }
        }

        if (type == SignType.Red)
        {
            for (int i = 0; i < trafficPedestrians.Length; i++)
            {
                trafficPedestrians[i].SetSign(SignType.Green);
            }
        }
    }

    protected override void WaitSign()
    {
        base.WaitSign();

        if (currentSign == SignType.Red)
        {
            if (nowTime < redSignTime)
            {
                nowTime += Time.deltaTime;
                return;
            }

            nowTime = 0.0f;
            SetSign(SignType.Yellow);
            return;
        }

        if (currentSign == SignType.Yellow)
        {
            if (nowTime < yellowSignTime)
            {
                nowTime += Time.deltaTime;
                return;
            }

            nowTime = 0.0f;
            SetSign(SignType.Green);
            return;
        }

        if (currentSign == SignType.Green)
        {
            if (nowTime < greenSignTime)
            {
                nowTime += Time.deltaTime;
                return;
            }

            nowTime = 0.0f;
            SetSign(SignType.Red);
            return;
        }
    }
}
