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
    public GameObject greenSign;
    public GameObject yellowSign;
    public GameObject redSign;

    public SignType currentSign { protected set; get; }

    public void ActiveGreenSign()
    {
        greenSign.SetActive(true);

        if(yellowSign)
            yellowSign.SetActive(false);

        redSign.SetActive(false);
    }

    public void ActiveRedSign()
    {
        greenSign.SetActive(false);

        if (yellowSign)
            yellowSign.SetActive(false);

        redSign.SetActive(true);
    }

    public void ActiveYellowSign()
    {
        greenSign.SetActive(false);

        if (yellowSign)
            yellowSign.SetActive(true);

        redSign.SetActive(false);
    }

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

        switch (type)
        {
            case SignType.Green:
                ActiveGreenSign();
                break;
            case SignType.Yellow:
                ActiveYellowSign();
                break; ;
            case SignType.Red:
                ActiveRedSign();
                break; ;
        }
    }

    protected virtual void WaitSign()
    {
        
    }
}
