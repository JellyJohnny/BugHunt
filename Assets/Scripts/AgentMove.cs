using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class AgentMove : MonoBehaviour
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
    public AudioSource overheadGunAud;
    public GameObject overheadProjectileManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();  
        
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
                    ModeManager.instance.moveIndicatorDecal.drawDistance = 0f;
                }
                else
                {
                    UpdateBool("isWalking", true);
                    UpdateBool("isIdle", false);
                    UpdateBool("isShooting", false);
                    ModeManager.instance.moveIndicatorDecal.drawDistance = 1000f;
                }
                overheadGunAud.Stop();
            }
            else
            {
                float _enemyDist = Vector3.Distance(transform.position, enemy.transform.position);
                if (agent.velocity.magnitude <= 0.1f && _enemyDist <= agent.stoppingDistance)
                {
                    UpdateBool("isWalking", false);
                    UpdateBool("isIdle", false);
                    UpdateBool("isShooting", true);
                    if (!overheadProjectileManager.activeSelf)
                    {
                        overheadProjectileManager.SetActive(true);
                    }
                    //play weapon sound
                    if (!overheadGunAud.isPlaying)
                    {
                        overheadGunAud.Play();
                    }
                    

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

                    overheadGunAud.Stop();
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
