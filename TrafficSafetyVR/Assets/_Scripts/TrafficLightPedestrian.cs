using UnityEngine;
using System.Collections;

public class TrafficLightPedestrian : TrafficLight
{
    public override void SetSign(SignType type)
    {
        base.SetSign(type);

        Debug.Log("Pedestrian sign changed : " + type);

        if (type == SignType.Green)
        {
        }
        if (type == SignType.Red)
        {
        }
    }

    public void Reset()
    {
        game.scene.state = SceneState.Play;
    }

    private void OnTriggerEnter(Collider enterColl)
    {
        if(!enterColl.CompareTag("Player"))
            return;

        if(currentSign == SignType.Green)
            return;

        //TODO : Go to fail;
    }
}
