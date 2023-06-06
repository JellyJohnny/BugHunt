using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerBase
{
    public override void EnterState(Player p)
    {
        Debug.Log("IDLE");
        p.anim.SetBool("isWalking", false);
        p.anim.SetBool("isShooting", false);
    }

    public override void UpdateState(Player p)
    {

    }
}
