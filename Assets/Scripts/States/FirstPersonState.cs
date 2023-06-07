using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        c.overheadCam.enabled = false;
        c.firstPersonCam.enabled = true;
        Camera.main.orthographic = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public override void UpdateState(ModeManager c)
    {

    }
}
