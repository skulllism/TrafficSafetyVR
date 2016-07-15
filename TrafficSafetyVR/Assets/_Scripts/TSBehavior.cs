using UnityEngine;
using System.Collections;

public class TSBehavior : MonoBehaviour
{
    public Game game { private set; get; }
    public TSCamera cam { private set; get; }
    public OVRCameraRig ovrCamRig { private set; get; }
    public InputManager input { private set; get; }

    protected virtual void Awake()
    {
        game = Game.Instance;
        cam = FindObjectOfType<TSCamera>();
        ovrCamRig = FindObjectOfType<OVRCameraRig>();
        input = FindObjectOfType(typeof(InputManager)) as InputManager;
    }

    public virtual void ManualUpdate()
    {
        
    }
}
