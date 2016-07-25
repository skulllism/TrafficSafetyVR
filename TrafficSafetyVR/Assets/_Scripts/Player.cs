using UnityEngine;
using System.Collections;

public class Player : Actor
{
    private PlayerFSM fsm;

    protected override void Awake()
    {
        base.Awake();
        fsm = GetComponent<PlayerFSM>();
    }

    private void JoystickCtrl()
    {
        Vector3 newDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        SetDirection(newDir);
    }

    private void MainButtonCtrl()
    {
        if (input.IsKeyDown(InputType.Main))
            SetDirection(new Vector3(transform.forward.x, transform.forward.y, transform.forward.z));
    }

    public void RotateHeadDirection()
    {
        if (Util.IsEditorPlatform())
        {
            transform.forward = new Vector3(cam.transform.forward.x, transform.forward.y, cam.transform.forward.z);
            return;
        }

        transform.forward = new Vector3(airVRCamRig.centerEyeAnchor.transform.forward.x, transform.forward.y, airVRCamRig.centerEyeAnchor.transform.forward.z);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();
        fsm.ManualUpdate();
        RotateHeadDirection();

        if (input.IsKeyDown(InputType.Joystick) || input.IsKeyDown(InputType.Main))
        {
            Accelate();
        }
        else
            Decelerate();

        if (input.IsKeyDown(InputType.Joystick))
        {
            JoystickCtrl();
            return;
        }

        if(input.IsKeyDown(InputType.Main))
        {
            MainButtonCtrl(); 
            return;
        }
        

    }
}
