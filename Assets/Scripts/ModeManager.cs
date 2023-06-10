using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    public CinemachineVirtualCamera firstPersonCam;
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    public float mouseX = 0f;
    public float mouseY = 0f;

    CinemachineVirtualCamera[] _vCams;


    [Header("PLAYER")]
    public NavMeshAgent currentAgent;

    [Header("UI")]
    public GameObject tabUI;
    public GameObject pauseMenu;

    //culling test



    private void Awake()
    {
        instance = this;
        currentState = strategyState;
        currentState.EnterState(this);
    }

    private void Start()
    {
        //currentAgent = GameObject.Find("Player").GetComponent<NavMeshAgent>();
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

        //disable all first person cams
        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
        _vCams = new CinemachineVirtualCamera[ _players.Length ];
        for (int i = 0; i < _players.Length; i++)
        {
            _vCams[i] = _players[i].GetComponent<Player>().fpCam;
        }

        for (int i = 0; i < _vCams.Length; i++)
        {
            _vCams[i].enabled = false;
        }

        if (canSwitch)
        {
            if (currentState == strategyState)
            {
                SwitchState(firstPersonState);
                StartCoroutine(Test2());
                
            }
            else
            {
                SwitchState(strategyState);
                StartCoroutine(Test1());

            }
            canSwitch = false;
            StartCoroutine(ResetTimescale());
        }
    }

    IEnumerator Test1()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "Player",
                    "AerialCam");
    }

    IEnumerator Test2()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI", "Player", "FirstPersonCam");
    }

    void OnEscape()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if(pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator ResetTimescale()
    {
        yield return new WaitForSeconds(1f);
        canSwitch = true;
        tabUI.SetActive(true);
    }
}
