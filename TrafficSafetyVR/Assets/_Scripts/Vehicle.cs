using System;
using UnityEngine;
using System.Collections;
using SWS;
using System.Collections.Generic;

public class Vehicle : Actor
{
    private Dictionary<VehicleType, GameObject> models = new Dictionary<VehicleType, GameObject>();

    public VehicleData data { private set; get; }
    public float startDelay;

    public  splineMove sm { private set; get; }
    public CarCheckBox checker { private set; get; }

    protected override void Awake()
    {
        base.Awake();
        sm = GetComponent<splineMove>();
        checker = GetComponentInChildren<CarCheckBox>();
    }

    public void InitVehicle()
    {
        UnityEngine.Object[] newResources = Resources.LoadAll("Vehicles");
        for (int i = 0; i < newResources.Length; i++)
        {
            GameObject newPrefab = newResources[i] as GameObject;
            VehicleType type = (VehicleType) Enum.Parse(typeof (VehicleType), newPrefab.name);
            GameObject newEnemy = GameObject.Instantiate(newPrefab, transform) as GameObject;
            newEnemy.transform.localPosition = Vector3.zero;
            models[type] = newEnemy;
        }
        Reset();
    }

    public void InitVehicle()
    {
        
    }

    public void StartMove()
    {
        StartCoroutine(WaitForSecondAndGo(startDelay , ()=> {sm.StartMove();}));
    }

    public void GoToTarget(Vector3 point)
    {
        SetAccel(1.0f);
        MakeDirection(point);
        transform.LookAt(transform.position + direction);
        game.scene.AddMissile(this);
    }

    public void GoToPlayer()
    {
        GoToTarget(new Vector3(game.scene.player.transform.position.x , transform.position.y , game.scene.player.transform.position.z));
    }

    private IEnumerator WaitForSecondAndGo(float waitTime , System.Action onComplete)
    {
        yield return new WaitForSeconds(waitTime);

        onComplete();
    }

    private VehicleType GetRandomType()
    {
        int random = UnityEngine.Random.Range(0, (int)VehicleType.Max);

        return (VehicleType)random;
    }

    public void Reset()
    {
        VehicleType randomType = GetRandomType();
        Debug.Log(randomType.ToString());

        for (int i = 0; i < (int) VehicleType.Max; i++)
        {
            VehicleType vehicleType = (VehicleType) i;
            models[vehicleType].SetActive(false);
        }

        models[randomType].SetActive(true);
        data = game.container.GetVehicleData(randomType);
        checker.transform.localPosition = new Vector3(0.0f , 1.0f , data.colliderZpos);
        sm.speed = data.speed;
    }
}
