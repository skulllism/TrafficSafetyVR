using UnityEngine;
using System.Collections;

public class CamRot : MonoBehaviour {

    public static CamRot instance;


    [SerializeField]
    Transform CameraObject;



    public float rotationXMargin = 25;
    public float rotationYMargin = 25;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CameraObject.localEulerAngles = new Vector3(Remap(Input.mousePosition.y, 0, Screen.height, rotationYMargin, -rotationYMargin),
                                                    Remap(Input.mousePosition.x, 0, Screen.width, -rotationXMargin, rotationXMargin),
                                                       0);
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}