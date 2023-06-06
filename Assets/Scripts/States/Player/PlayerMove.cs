using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerBase
{
    public override void EnterState(Player p)
    {
        Debug.Log("MOVE");
        p.anim.SetBool("isIdle", false);
        p.anim.SetBool("isWalking", true);
        p.agent.SetDestination(p.agentDestination);
    }

    public override void UpdateState(Player p)
    {
        if(p.agent.remainingDistance <= p.agent.stoppingDistance)
        {
            if(p.currentEnemy != null) 
            {
                p.SwitchState(p.attackState);
            }
            else
            {
                p.SwitchState(p.idleState);
            }

        }
    }
}
