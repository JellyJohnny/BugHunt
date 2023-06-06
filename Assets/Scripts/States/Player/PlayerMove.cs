using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerBase
{
    public override void EnterState(Player p)
    {
        Debug.Log("MOVE");
        p.anim.SetBool("isWalking", true);
        p.anim.SetBool("isShooting", false);

        var data = p.aimConstraint.data.sourceObjects;
        data.SetTransform(0,null);
        p.aimConstraint.data.sourceObjects = data;
        p.rigBuilder.Build();
    }

    public override void UpdateState(Player p)
    {
        float _destinationDistance = Vector3.Distance(p.transform.position, p.agent.destination);
        if (p.currentEnemy != null)
        {
            if (_destinationDistance <= (p.agent.GetComponent<Player>().minAttackDistance))
            {
                p.SwitchState(p.attackState);
            }
        }
        else
        {
            if (_destinationDistance <= (p.agent.GetComponent<Player>().minStopDistance))
            {
                p.SwitchState(p.idleState);
            }
        }
    }
}
