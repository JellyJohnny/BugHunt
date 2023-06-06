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
    
    [Header("STATES")]
    public PlayerBase currentState;
    public PlayerIdle idleState = new PlayerIdle();
    public PlayerMove moveState = new PlayerMove();
    public PlayerAttack attackState = new PlayerAttack();

    [Header("AUDIO")]
    public AudioSource aud;
    public AudioClip[] playerSelected;
    public AudioClip[] moveToLocation;
    public AudioClip engageEnemy;

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

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
