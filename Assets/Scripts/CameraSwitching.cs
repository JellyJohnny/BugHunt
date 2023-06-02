using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitching : MonoBehaviour
{
    public CinemachineVirtualCamera overheadCam;
    public CinemachineVirtualCamera testCam;
    public SkinnedMeshRenderer playerMeshRender;

    public void UpdateCameras(CinemachineVirtualCamera virtualCam,bool b1, CinemachineVirtualCamera virtualCam2, bool b2)
    {
        virtualCam.enabled = b1;
        virtualCam2.enabled = b2;

        if(overheadCam.enabled)
        {
            testCam.transform.parent.GetComponent<OverAnimation>().agent.enabled = true;
            testCam.transform.parent.GetComponent<OverAnimation>().canUpdateAnimation = true;
            Debug.Log("overhead");
            playerMeshRender.forceRenderingOff = false;
        }
        else
        {
            testCam.transform.parent.GetComponent<OverAnimation>().agent.enabled = false;
            testCam.transform.parent.GetComponent<CharacterController>().enabled = true;
            Debug.Log("first person");
            playerMeshRender.forceRenderingOff = true;
        }
    }

    void OnCamSwitch()
    {
        UpdateCameras(overheadCam, !overheadCam.enabled, testCam, !testCam.enabled);
    }
}
