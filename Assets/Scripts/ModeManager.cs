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
    public static ModeManager instance;
    AudioSource aud;
    bool canSwitch = true;

    [Header("STATES")]
    public BaseState currentState;
    public StrategyState strategyState = new StrategyState();
    public FirstPersonState firstPersonState = new FirstPersonState();

    [Header("STRATEGY")]
    public CinemachineVirtualCamera overheadCam;
    public GameObject moveIndicator;
    public LayerMask layerToIgnore;
    public DecalProjector moveIndicatorDecal;
    public float minimumShootingDistance;
    public float minimumStopDistance;

    [Header("FIRST PERSON")]
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    public float mouseX = 0f;
    public float mouseY = 0f;

    
    [Header("PLAYER")]
    public NavMeshAgent currentAgent;

    /*
    public AudioSource currentPlayerAudio;
    public CinemachineVirtualCamera currentFirstPersonCamera;
    public AudioClip[] currentPlayerAudioClips;
    public Transform currentPlayerBody;
    public SkinnedMeshRenderer currentPlayerMeshRenderer;
    public GameObject currentOverheadGun;
    public GameObject currentFirstPersonGun;
    public AgentMove currentOverheadAnimator;
    public Animator firstPersonGunAnimator;
    public float muzzleFlashDuration;
    public Animator firstPersonMuzzleAnimator;
    public Animator firstPersonMuzzleLightAnimator;
    public GameObject firstPersonMuzzleObject;
    public AudioSource firstPersonAudio;
    public GameObject firstPersonProjectileManager;
    public GameObject firstPersonParentObject;
    */
    [Header("UI")]
    public GameObject tabUI;

    private void Start()
    {
        currentAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
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

    void OnCamSwitch()
    {
        tabUI.SetActive(false);
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
        tabUI.SetActive(true);
    }
}
