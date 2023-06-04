using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class ModeManager : MonoBehaviour
{

    //STATES
    public BaseState currentState;
    public StrategyState strategyState = new StrategyState();
    public FirstPersonState firstPersonState = new FirstPersonState();
    //ENDSTATES

    bool canSwitch = true;

    public static ModeManager instance;
    public CinemachineVirtualCamera overheadCam;
    public CinemachineVirtualCamera firstPersonCam;
    public SkinnedMeshRenderer playerMeshRender;
    //public MouseLook mouseLook;
    public GameObject gunOverhead;
    public GameObject gunFP;
    AudioSource aud;

    //overhead stuff
    public NavMeshAgent agent;
    public GameObject moveIndicator;
    public LayerMask layerMask;
    public DecalProjector dp;
    public float shootDistance;
    public float stopDistance;
    public AudioSource playerAud;
    public AudioClip[] clips;
    public OverAnimation overAnim;

    //mouse stuff
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;
    public Animator gunAnim;

    float timer;
    public float muzzleFlashDuration;
    public Animator muzzlAnim;
    public Animator muzzleLightAnim;
    public GameObject muzzleLight;
    public AudioSource firstPersonAud;
    public GameObject projectileManager;

    //MY VARIABLES
    public float mouseX = 0f;
    public float mouseY = 0f;
    public GameObject mouseLookParent;

    private void Start()
    {
        instance = this;
        currentState = strategyState;
        currentState.EnterState(this);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        aud = GetComponent<AudioSource>();  
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public IEnumerator WeaponVisibleDelay()
    {
        yield return new WaitForSeconds(1f);
        gunFP.SetActive(true);
    }

    void OnCamSwitch()
    {
        if (canSwitch)
        {
            if (currentState == strategyState)
            {
                SwitchState(firstPersonState);
            }
            else
            {
                SwitchState(strategyState);
            }
            canSwitch = false;
            StartCoroutine(ResetTimescale());
        }
    }

    IEnumerator ResetTimescale()
    {
        yield return new WaitForSeconds(1f);
        canSwitch = true;
    }

    public void ChooseRandomVoiceClip()
    {
        int _rand = Random.Range(0, clips.Length);

        playerAud.clip = clips[_rand];
        playerAud.Play();
    }
}
