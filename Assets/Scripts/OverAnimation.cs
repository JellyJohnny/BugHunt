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
    public GameObject enemy;
    public GameObject muzzle;
    public float muzzleDelayTime;
    public float muzzleFlashDuration;
    public float rotationSpeed;

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
            if (enemy == null)
            {
                if (agent.velocity.magnitude <= 0.1f)
                {
                    UpdateBool("isWalking", false);
                    UpdateBool("isIdle", true);
                    UpdateBool("isShooting", false);
                    OverHeadMouse.instance.dp.drawDistance = 0f;
                }
                else
                {
                    UpdateBool("isWalking", true);
                    UpdateBool("isIdle", false);
                    UpdateBool("isShooting", false);
                    OverHeadMouse.instance.dp.drawDistance = 1000f;
                }
            }
            else
            {
                float _enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
                if (agent.velocity.magnitude <= 0.1f && _enemyDist <= agent.stoppingDistance)
                {
                    UpdateBool("isWalking", false);
                    UpdateBool("isIdle", false);
                    UpdateBool("isShooting", true);
                    //rotate towards enemy
                    Vector3 _heading = enemy.transform.position - transform.position;
                    Quaternion _lookRotation = Quaternion.LookRotation(_heading,Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    UpdateBool("isWalking", true);
                    UpdateBool("isIdle", false);
                    UpdateBool("isShooting", false);
                }
            }
        }
    }

    public void StartMuzzleDelay()
    {
        StartCoroutine(MuzzleFLashDelay());
    }

    IEnumerator MuzzleFLashDelay()
    {
        muzzle.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzle.SetActive(false);
    }
}
