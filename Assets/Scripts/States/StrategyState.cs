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
        Camera.main.orthographic = true;
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
                            //Debug.Log("bug");
                            c.currentAgent.GetComponent<Player>().currentEnemy = hit.collider.gameObject;
                            c.currentAgent.stoppingDistance = c.currentAgent.GetComponent<Player>().minAttackDistance;
                            c.currentAgent.GetComponent<Player>().SwitchState(c.currentAgent.GetComponent<Player>().moveState);
                            c.currentAgent.SetDestination(hit.point);
                            break;
                        case "Player":
                            c.currentAgent = hit.collider.gameObject.GetComponent<NavMeshAgent>(); //update the mode manager with selected player
                            c.firstPersonCam = c.currentAgent.GetComponent<Player>().fpCam;
                            c.currentAgent.GetComponent<Player>().PlayerSelected(); //run the strobe function on the player
                            c.currentAgent.GetComponent<Player>().currentEnemy = null;
                            c.currentAgent.stoppingDistance = c.currentAgent.GetComponent<Player>().minStopDistance;
                            c.overheadCam.Follow = hit.collider.gameObject.transform;
                            c.overheadCam.LookAt = hit.collider.gameObject.transform;
                            break;
                        case "Untagged":
                            //Debug.Log("ground");
                            c.currentAgent.GetComponent<Player>().currentEnemy = null;
                            c.currentAgent.stoppingDistance = c.currentAgent.GetComponent<Player>().minStopDistance;
                            //Move the indicator
                            c.moveIndicator.GetComponent<MoveIndicator>().enabled = false;
                            c.moveIndicator.transform.position = hit.point;
                            c.moveIndicator.GetComponent<MoveIndicator>().enabled = true;
                            c.currentAgent.GetComponent<Player>().SwitchState(c.currentAgent.GetComponent<Player>().moveState);
                            c.currentAgent.SetDestination(hit.point);
                            break;
                    }
                    
                }
            }
        }
    }
}
