﻿using UnityEngine;
using System.Collections;

public class TSCamera : FSMBase
{
    public Vector2 rotationMax;
    public float mouseSensitivity;
    public float rotSpeed;

    public float followSensitivity;
    public Vector3 baseOffSet;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private void FollowPlayer()
    {
        Vector3 newPos = game.scene.player.transform.position;
        Vector3 fixOffSet = game.scene.player.transform.forward * baseOffSet.z + Vector3.up * baseOffSet.y;
        transform.position = Vector3.Slerp(transform.position, newPos + fixOffSet, Time.deltaTime * followSensitivity);
    }

    private void PcRotate()
    {
        rotY += Input.GetAxis("Mouse Y") * mouseSensitivity;

                 if (rotY > rotationMax.y)
                     rotY = rotationMax.y;
         
                 if (rotY < -rotationMax.y)
                     rotY = -rotationMax.y;

        rotX += Input.GetAxis("Mouse X") * mouseSensitivity;
        Quaternion newRotation = Quaternion.Euler(-rotY, rotX, 0.0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, Time.deltaTime * rotSpeed);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();
        FollowPlayer();

        if (!Util.IsEditorPlatform())
            return;

        PcRotate();
    }
}
