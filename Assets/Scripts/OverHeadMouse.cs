using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class OverHeadMouse : MonoBehaviour
{
    public static OverHeadMouse instance;   
    public NavMeshAgent agent;
    public GameObject moveIndicator;
    public LayerMask layerMask;
    public DecalProjector dp;
    public float shootDistance;
    public float stopDistance;
    public AudioSource playerAud;
    public AudioClip[] clips;

    private void Start()
    {
        instance = this;
        dp = moveIndicator.GetComponent<DecalProjector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && CameraSwitching.instance.overheadCam.enabled)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f, ~layerMask))
            {
                if (hit.collider.gameObject.tag != "Enemy")
                {
                    agent.GetComponent<OverAnimation>().enemy = null;
                    agent.SetDestination(hit.point);
                    agent.stoppingDistance = stopDistance;

                    moveIndicator.transform.position = new Vector3(hit.point.x, moveIndicator.transform.position.y, hit.point.z);
                    moveIndicator.GetComponent<AudioSource>().Play();
                    //dp.drawDistance = 1000f;
                    agent.GetComponent<OverAnimation>().UpdateCharactercontroller(false);
                    agent.GetComponent<OverAnimation>().canUpdateAnimation = true;
                    
                }
                else
                {
                    Debug.Log("clicked bug");
                    agent.SetDestination(hit.collider.gameObject.transform.position);
                    agent.stoppingDistance = shootDistance;
                    agent.GetComponent<OverAnimation>().enemy = hit.collider.gameObject;
                    ChooseRandomVoiceClip();
                }
            }
        }
    }

    void ChooseRandomVoiceClip()
    {
        int _rand = UnityEngine.Random.Range(0,clips.Length);

        playerAud.clip = clips[_rand];
        playerAud.Play();
    }
}
