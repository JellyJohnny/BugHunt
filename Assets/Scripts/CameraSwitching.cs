using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitching : MonoBehaviour
{
    public static CameraSwitching instance;
    public CinemachineVirtualCamera overheadCam;
    public CinemachineVirtualCamera testCam;
    public SkinnedMeshRenderer playerMeshRender;
    public MouseLook mouseLook;
    public GameObject gunOverhead;
    public GameObject gunFP;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void UpdateCameras(CinemachineVirtualCamera virtualCam,bool b1, CinemachineVirtualCamera virtualCam2, bool b2)
    {
        virtualCam.enabled = b1;
        virtualCam2.enabled = b2;

        if(overheadCam.enabled)
        {
            testCam.transform.parent.transform.parent.GetComponent<OverAnimation>().agent.enabled = true;
            testCam.transform.parent.transform.parent.GetComponent<OverAnimation>().canUpdateAnimation = true;
            Debug.Log("overhead");
            playerMeshRender.forceRenderingOff = false;
            gunOverhead.SetActive(true);
            gunFP.SetActive(false);
            mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            testCam.transform.parent.transform.parent.GetComponent<OverAnimation>().agent.enabled = false;
            testCam.transform.parent.transform.parent.GetComponent<CharacterController>().enabled = true;
            Debug.Log("first person");
            playerMeshRender.forceRenderingOff = true;
            gunOverhead.SetActive(false);
            mouseLook.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(WeaponVisibleDelay());
        }
    }

    IEnumerator WeaponVisibleDelay()
    {
        yield return new WaitForSeconds(1f);
        gunFP.SetActive(true);
    }

    void OnCamSwitch()
    {
        UpdateCameras(overheadCam, !overheadCam.enabled, testCam, !testCam.enabled);
    }
}
