using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class OverAnimation : MonoBehaviour
{
    Animator anim;
    public NavMeshAgent agent;
    CharacterController characterController;
    public bool canUpdateAnimation = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();  
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void UpdateCharactercontroller(bool en)
    {
        characterController.enabled = en;
    }

    public void UpdateBool(string s, bool b)
    {
        anim.SetBool(s, b);
    }

    private void Update()
    {
        if (agent.enabled)
        {
            if (agent.remainingDistance <= 0.1f)
            {
                UpdateBool("isWalking", false);
                UpdateBool("isIdle", true);
                OverHeadMouse.instance.dp.drawDistance = 0f;
            }
            else
            {
                UpdateBool("isWalking", true);
                UpdateBool("isIdle", false);
                OverHeadMouse.instance.dp.drawDistance = 1000f;
            }
        }
    }
}
