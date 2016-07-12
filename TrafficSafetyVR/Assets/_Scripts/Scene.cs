using UnityEngine;
using System.Collections;

public class Scene : TSBehavior
{
    protected override void Awake()
    {
        base.Awake();
        game.SetScene(this);
    } 
}
