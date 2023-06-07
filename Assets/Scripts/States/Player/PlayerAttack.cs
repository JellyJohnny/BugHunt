using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAttack : PlayerBase
{
    public override void EnterState(Player p)
    {
        //Debug.Log("ATTACK");
        p.anim.SetBool("isShooting", true);
        p.anim.SetFloat("shootSpeed", p.shootingSpeed);
        var data = p.aimConstraint.data.sourceObjects;
        data.SetTransform(0, p.currentEnemy.transform.GetChild(2).transform);
        p.aimConstraint.data.sourceObjects = data;
        p.rigBuilder.Build();
    }

    public override void UpdateState(Player p)
    {
        Vector3 heading = p.currentEnemy.transform.position - p.transform.position;
        Quaternion _lookRotation = Quaternion.LookRotation(heading,Vector3.up);
        p.transform.rotation = Quaternion.Slerp(p.transform.rotation, _lookRotation, p.turnSpeed * Time.deltaTime);

        if(p.currentEnemy == null || p.currentEnemy.GetComponent<EnemyMovement>().myHealth <= 0)
        {
            p.SwitchState(p.idleState);
        }
    }
}
