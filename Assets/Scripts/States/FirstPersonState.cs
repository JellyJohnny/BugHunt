using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        /*
        c.currentAgent.GetComponent<CharacterController>().enabled = true;
        c.overheadCam.enabled = false;
        c.currentFirstPersonCamera.enabled = true;
        Debug.Log("first person state");
        c.currentFirstPersonCamera.transform.parent.transform.parent.GetComponent<AgentMove>().agent.enabled = false;
        c.currentFirstPersonCamera.transform.parent.transform.parent.GetComponent<CharacterController>().enabled = true;
        Debug.Log("first person");
        c.currentPlayerMeshRenderer.forceRenderingOff = true;
        c.currentOverheadGun.SetActive(false);
        //c.mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        c.StartCoroutine(c.WeaponVisibleDelay());
        */
    }

    public override void UpdateState(ModeManager c)
    {
        /*
        c.mouseX = Input.GetAxis("Mouse X");
        c.mouseY = Input.GetAxis("Mouse Y");
        float MouseX = c.mouseX * c.mouseSensitivity * Time.deltaTime;
        float MouseY = c.mouseY * c.mouseSensitivity * Time.deltaTime;

        c.xRotation -= MouseY;
        c.xRotation = Mathf.Clamp(c.xRotation, -90f, 90f);

        c.firstPersonParentObject.transform.localRotation = Quaternion.Euler(c.xRotation, 0f, 0f);
        c.currentPlayerBody.Rotate(Vector3.up * MouseX);

        if (Input.GetMouseButton(0))
        {
            if (!c.firstPersonGunAnimator.GetBool("isFiring"))
            {
                c.firstPersonGunAnimator.SetBool("isFiring", true);
                c.firstPersonMuzzleAnimator.SetBool("isFlashing", true);
                c.firstPersonMuzzleObject.SetActive(true);
                c.firstPersonProjectileManager.SetActive(true);
                if (!c.firstPersonAudio.isPlaying)
                {
                    c.firstPersonAudio.Play();
                }
            }
        }
        else
        {
            if (c.firstPersonGunAnimator.GetBool("isFiring"))
            {
                c.firstPersonGunAnimator.SetBool("isFiring", false);
                c.firstPersonMuzzleAnimator.SetBool("isFlashing", false);
                c.firstPersonMuzzleObject.SetActive(false);
                c.firstPersonProjectileManager.SetActive(false);
                if (c.firstPersonAudio.isPlaying)
                {
                    c.firstPersonAudio.Stop();
                }
            }
        }
        */
    }
}
