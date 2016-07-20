using UnityEngine;
using System.Collections;

public enum SignType
{
    Red,
    Yellow,
    Green,
}

public class TrafficLight : TSBehavior
{
    public SignType currentSign { protected set; get; }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        WaitSign();
    }

    public bool IsRedSign()
    {
        return currentSign == SignType.Red;
    }

    public virtual void SetSign(SignType type)
    {
        //TODO : Direct

        currentSign = type;
    }

    protected virtual void WaitSign()
    {
        
    }
}
