using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerBase
{
    public override void EnterState(Player p)
    {
        Debug.Log("ATTACK");
        p.anim.SetBool("isShooting", true);
        p.anim.SetBool("isIdle", false);
        p.anim.SetBool("isWalking", false);
    }

    public override void UpdateState(Player p)
    {
        if(p.currentEnemy == null)
        {
            p.SwitchState(p.idleState);
        }
    }
}
