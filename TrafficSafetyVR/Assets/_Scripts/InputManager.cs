﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum InputType
{
    Joystick,
    Main,
    Max
}

public class InputManager : TSBehavior
{
    private static InputManager instance = null;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }

    private Dictionary<InputType , bool> inputs = new Dictionary<InputType, bool>();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < (int) InputType.Max; i++)
        {
            InputType type = (InputType) i;
            inputs.Add(type, false);
        }
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        bool joystick = Mathf.Abs(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) > 0 ? true : false;

        if (joystick)
        {
            if(!IsKeyDown(InputType.Joystick))
                KeyDown(InputType.Joystick);
        }
        else
            KeyUp(InputType.Joystick);

        if(Input.GetMouseButtonDown(0) && !IsKeyDown(InputType.Main))
            KeyDown(InputType.Main);
        if(Input.GetMouseButtonUp(0) && IsKeyDown(InputType.Main))
            KeyUp(InputType.Main);
    }

    public void KeyDown(InputType type)
    {
        inputs[type] = true;
    }

    public void KeyUp(InputType type)
    {
        inputs[type] = false;
    }

    public bool IsKeyDown(InputType type)
    {
        return inputs[type];
    }
}
