using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public CharacterController charController;
    public Animator anim;
    
    public FirstPersonMove fpMove;
    public GameObject currentEnemy;
    public GameObject currentInteractable;

    [Header("CAMERA")]
    public CinemachineVirtualCamera fpCam;

    [Header("STATES")]
    public PlayerBase currentState;
    public PlayerIdle idleState = new PlayerIdle();
    public PlayerMove moveState = new PlayerMove();
    public PlayerAttack attackState = new PlayerAttack();

    [Header("AUDIO")]
    public AudioSource aud;
    public AudioSource weaponAudio;
    public AudioClip[] playerSelected;
    public AudioClip[] moveToLocation;
    public AudioClip engageEnemy;
    public AudioClip weaponFire;

    [Header("VISIBILITY")]
    public GameObject playerMesh;
    public GameObject gunMesh;

    [Header("AGENT")]
    public NavMeshAgent agent;
    public float minStopDistance;
    public float minAttackDistance;
    public AgentMove agentMove;

    [Header("RIG")]
    public RigBuilder rigBuilder;
    public MultiAimConstraint aimConstraint;
    public float aimSpeed;
    public float constraintWeight = 0f;

    [Header("SHOOTING")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float shootingSpeed;
    public float weaponSpread;
    public Transform projectileOrigin;
    public GameObject muzzleObject;

    public float turnSpeed;

    private void Start()
    {
        charController = GetComponent<CharacterController>();   
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        fpMove = GetComponent<FirstPersonMove>(); 
        agentMove = GetComponent<AgentMove>();

        currentState = idleState;
        currentState.EnterState(this);
    }

    public void PlayerSelected()
    {
        StartCoroutine(StrobePlayer());
    }

    IEnumerator StrobePlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            playerMesh.SetActive(false);
            gunMesh.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            playerMesh.SetActive(true);
            gunMesh.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
            
    }

    public void PlayAudioClip(string tag)
    {
        switch (tag)
        {
            case "Enemy":
                aud.clip = engageEnemy;
                aud.Play();
                break;
            case "Player":
                int _rand = Random.Range(0, playerSelected.Length);
                aud.clip = playerSelected[_rand];
                aud.Play();
                break;
            case "Untagged":
                int _rand2 = Random.Range(0, moveToLocation.Length);
                aud.clip = moveToLocation[_rand2];
                aud.Play();
                break;
        }

    }

    public void FireProjectile()
    {
        //Debug.Log("Projectile!");

        GameObject _proj = Instantiate(projectilePrefab, projectileOrigin.transform.position, Quaternion.identity);

        Vector3 _rand = Random.insideUnitSphere * weaponSpread;
        Vector3 _vel = Vector3.zero;

        if (currentEnemy != null)
        {
            _vel = (new Vector3(currentEnemy.transform.position.x, currentEnemy.transform.position.y, currentEnemy.transform.position.z) - transform.position) + _rand;
        }
        else
        {
            _vel = transform.forward + _rand;
        }
        _proj.GetComponent<Rigidbody>().velocity = _vel * projectileSpeed;
        weaponAudio.clip = weaponFire;
        weaponAudio.Play();
        muzzleObject.SetActive(true);
        StartCoroutine(HideMuzzle());
    }

    IEnumerator HideMuzzle()
    {
        yield return new WaitForSeconds(shootingSpeed * 0.2f);
        muzzleObject.SetActive(false);
    }

    private void Update()
    {
        currentState.UpdateState(this);

        anim.SetFloat("shootSpeed", shootingSpeed);
    }

    public void SwitchState(PlayerBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
