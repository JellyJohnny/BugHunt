using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public float stunDuration;
    int myHealth = 3;
    public Material defaultMaterial;
    public Material hitMaterial;
    public float flashDuration;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    public void TakeDamage()
    {
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
        StartCoroutine(ResetMaterial());
        myHealth--;

        if(myHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("isDead");
            agent.enabled = false;
        }
        agent.enabled = false;
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = defaultMaterial;
    }
}
