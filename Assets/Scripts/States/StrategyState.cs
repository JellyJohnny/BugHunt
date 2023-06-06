using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StrategyState : BaseState
{
    public override void EnterState(ModeManager c)
    {
        //c.currentAgent.GetComponent<CharacterController>().enabled = false;
        c.overheadCam.enabled = true;
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

            if (Physics.Raycast(ray, out hit, 1000f, ~c.layerToIgnore))
            {
                if (hit.collider != null)
                {
                    c.currentAgent.GetComponent<Player>().PlayAudioClip(hit.collider.gameObject.tag.ToString());
                    switch (hit.collider.gameObject.tag)
                    {
                        
                        case "Enemy":
                            Debug.Log("bug");
                            c.currentAgent.GetComponent<Player>().currentEnemy = hit.collider.gameObject;
                            
                            break;
                        case "Player":
                            c.overheadCam.Follow = hit.collider.gameObject.transform;
                            c.overheadCam.LookAt = hit.collider.gameObject.transform;
                            c.currentAgent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
                            c.currentAgent.GetComponent<Player>().PlayerSelected();
                            break;
                        default:
                            Debug.Log("ground");
                            c.currentAgent.GetComponent<Player>().agentDestination = hit.point;
                            c.currentAgent.GetComponent<Player>().SwitchState(c.currentAgent.GetComponent<Player>().moveState);

                            c.moveIndicator.GetComponent<MoveIndicator>().enabled = false;
                            c.moveIndicator.transform.position = hit.point;
                            c.moveIndicator.GetComponent<MoveIndicator>().enabled = true;
                            break;
                    }
                    
                }
            }
        }
    }
}
