using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Actor : TSBehavior
{
    public Vector3 direction { private set; get; }
    public float speed;
    public float accel { private set; get; }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    public void MakeDirection(Vector3 point)
    {
        SetDirection((point - transform.position).normalized);
    }

    public void SetAccel(float accel)
    {
        this.accel = accel;
    }

    public void Accelate()
    {
        if(accel >= 1.0f)
            return;

        accel += Time.deltaTime * 5;
    }

    public void Decelerate()
    {
        if (accel < 0.0f)
        {
            accel = 0.0f;
            return;
        }
        if(accel <= 0.0f)
            return;

        accel -= Time.deltaTime * 10;
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        transform.position += direction*Time.deltaTime*speed*accel;
    }
}
