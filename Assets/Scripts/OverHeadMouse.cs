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
                agent.SetDestination(hit.point);
                moveIndicator.transform.position = new Vector3(hit.point.x, moveIndicator.transform.position.y, hit.point.z);
                //dp.drawDistance = 1000f;
                agent.GetComponent<OverAnimation>().UpdateCharactercontroller(false);
                agent.GetComponent<OverAnimation>().canUpdateAnimation = true;
            }
        }
    }
}
