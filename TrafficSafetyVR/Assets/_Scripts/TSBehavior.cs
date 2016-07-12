using UnityEngine;
using System.Collections;

public class TSBehavior : MonoBehaviour
{
    public Game game { private set; get; }

    protected virtual void Awake()
    {
        game = Game.Instance;
    }

    public virtual void ManualUpdate()
    {
        
    }
}
