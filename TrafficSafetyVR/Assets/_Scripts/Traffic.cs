using UnityEngine;
using System.Collections;

public class Traffic : TSBehavior
{
    private TrafficLightCar[] trafficLightCars;
    private TrafficLightPedestrian[] trafficLightPedestrians;

    protected override void Awake()
    {
        base.Awake();

        game.SetTraffic(this);
        trafficLightCars = FindObjectsOfType<TrafficLightCar>();
        trafficLightPedestrians = FindObjectsOfType<TrafficLightPedestrian>();
    }

    public void SpawnTrafficWave(int waveIndex)
    {
        
    }

    public void Init()
    {
        for (int i = 0; i < trafficLightCars.Length; i++)
        {
            game.scene.AddTrafficLight(trafficLightCars[i]); 
        }

        for (int i = 0; i < trafficLightPedestrians.Length; i++)
        {
            game.scene.AddTrafficLight(trafficLightPedestrians[i]);
        }
    }

    public TrafficLightCar FindNearTrafficLightCar(Actor actor)
    {
        float tmp = float.MaxValue;
        TrafficLightCar result = null;

        for (int i = 0; i < trafficLightCars.Length; i++)
        {
            float distance = Vector3.Distance(actor.transform.position, trafficLightCars[i].transform.position);
            if (distance < tmp)
            {
                tmp = distance;
                result = trafficLightCars[i];
            }
        }

        return result;
    }

    public TrafficLightPedestrian FindNearTrafficLightPedestrian(Actor actor)
    {
        float tmp = float.MaxValue;
        TrafficLightPedestrian result = null;

        for (int i = 0; i < trafficLightCars.Length; i++)
        {
            float distance = Vector3.Distance(actor.transform.position, trafficLightCars[i].transform.position);
            if (distance < tmp)
            {
                tmp = distance;
                result = trafficLightPedestrians[i];
            }
        }

        return result;
    }
}
