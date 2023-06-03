using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        c.agent.GetComponent<CharacterController>().enabled = true;
        c.overheadCam.enabled = false;
        c.firstPersonCam.enabled = true;
        Debug.Log("first person state");
        c.firstPersonCam.transform.parent.transform.parent.GetComponent<OverAnimation>().agent.enabled = false;
        c.firstPersonCam.transform.parent.transform.parent.GetComponent<CharacterController>().enabled = true;
        Debug.Log("first person");
        c.playerMeshRender.forceRenderingOff = true;
        c.gunOverhead.SetActive(false);
        //c.mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        c.StartCoroutine(c.WeaponVisibleDelay());
    }

    public override void UpdateState(ModeManager c)
    {
        c.mouseX = Input.GetAxis("Mouse X");
        c.mouseY = Input.GetAxis("Mouse Y");
        float MouseX = c.mouseX * c.mouseSensitivity * Time.deltaTime;
        float MouseY = c.mouseY * c.mouseSensitivity * Time.deltaTime;

        c.xRotation -= MouseY;
        c.xRotation = Mathf.Clamp(c.xRotation, -90f, 90f);

        c.mouseLookParent.transform.localRotation = Quaternion.Euler(c.xRotation, 0f, 0f);
        c.playerBody.Rotate(Vector3.up * MouseX);

        if (Input.GetMouseButton(0))
        {
            if (!c.gunAnim.GetBool("isFiring"))
            {
                c.gunAnim.SetBool("isFiring", true);
                c.muzzlAnim.SetBool("isFlashing", true);
                c.muzzleLight.SetActive(true);
                c.projectileManager.SetActive(true);
                if (!c.firstPersonAud.isPlaying)
                {
                    c.firstPersonAud.Play();
                }
            }
        }
        else
        {
            if (c.gunAnim.GetBool("isFiring"))
            {
                c.gunAnim.SetBool("isFiring", false);
                c.muzzlAnim.SetBool("isFlashing", false);
                c.muzzleLight.SetActive(false);
                c.projectileManager.SetActive(false);
                if (c.firstPersonAud.isPlaying)
                {
                    c.firstPersonAud.Stop();
                }
            }
        }
    }
}
