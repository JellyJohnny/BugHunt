using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        c.agent.GetComponent<CharacterController>().enabled = false;
        c.overheadCam.enabled = true;
        c.firstPersonCam.enabled = false;
        Debug.Log("strategy state");
        c.firstPersonCam.transform.parent.transform.parent.GetComponent<OverAnimation>().agent.enabled = true;
        c.firstPersonCam.transform.parent.transform.parent.GetComponent<OverAnimation>().canUpdateAnimation = true;
        Debug.Log("overhead");
        c.playerMeshRender.forceRenderingOff = false;
        c.gunOverhead.SetActive(true);
        c.gunFP.SetActive(false);
        //c.mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public override void UpdateState(ModeManager c)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, ~c.layerMask))
            {
                if (hit.collider.gameObject.tag != "Enemy")
                {
                    c.overAnim.overheadProjectileManager.SetActive(false);
                    c.agent.GetComponent<OverAnimation>().enemy = null;
                    c.agent.SetDestination(hit.point);
                    c.agent.stoppingDistance = c.stopDistance;

                    c.moveIndicator.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    c.moveIndicator.GetComponent<AudioSource>().Play();
                    //dp.drawDistance = 1000f;
                    c.agent.GetComponent<OverAnimation>().UpdateCharactercontroller(false);
                    c.agent.GetComponent<OverAnimation>().canUpdateAnimation = true;

                }
                else
                {
                    Debug.Log("clicked bug");
                    //overAnim.overheadProjectileManager.SetActive(false);
                    c.agent.SetDestination(hit.collider.gameObject.transform.position);
                    c.agent.stoppingDistance = c.shootDistance;
                    c.agent.GetComponent<OverAnimation>().enemy = hit.collider.gameObject;
                    c.ChooseRandomVoiceClip();
                }
            }
        }
    }
}
