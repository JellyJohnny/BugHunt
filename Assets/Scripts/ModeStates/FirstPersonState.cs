using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class FirstPersonState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        
        
        c.overheadCam.enabled = false;
        c.firstPersonCam.enabled = true;
        //Camera.main.orthographic = false;
        c.currentAgent.GetComponent<CharacterController>().enabled = true;
        c.currentAgent.GetComponent<NavMeshAgent>().enabled = false;
        c.currentAgent.GetComponent<Player>().fpMove.enabled = true;
        c.currentAgent.GetComponent<Player>().mLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void UpdateState(ModeManager c)
    {

    }
}
