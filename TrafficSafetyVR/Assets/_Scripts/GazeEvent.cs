using UnityEngine;
using System.Collections;

public class GazeEvent : TSBehavior
{
    public bool complete { private set; get; }

    public void Reset()
    {
        complete = false;
    }

    public void Complete()
    {
        complete = true;
    }
}

