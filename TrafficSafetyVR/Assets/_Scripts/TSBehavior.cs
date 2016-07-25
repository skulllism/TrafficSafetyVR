using UnityEngine;
using System.Collections;

public class TSBehavior : MonoBehaviour
{
    public Game game { private set; get; }
    public TSCamera cam { private set; get; }
    public AirVRCameraRig airVRCamRig { private set; get; }
    public AirVRClient airVRClient { private set; get; }
    public InputManager input { private set; get; }

    protected virtual void Awake()
    {
        game = Game.Instance;
        cam = FindObjectOfType<TSCamera>();
        airVRCamRig = FindObjectOfType<AirVRCameraRig>();
        airVRClient = FindObjectOfType<AirVRClient>();
        input = FindObjectOfType(typeof(InputManager)) as InputManager;
    }

    public virtual void ManualUpdate()
    {
        
    }
}
