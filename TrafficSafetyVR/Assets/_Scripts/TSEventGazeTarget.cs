using UnityEngine;
using System.Collections;

public class TSEventGazeTarget : TSBehavior
{
    public bool clear { private set; get; }

    public void Clear()
    {
        clear = true;
    }
}
