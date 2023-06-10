using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public float stunDuration;
    public int myHealth = 3;
    public Material defaultMaterial;
    public Material hitMaterial;
    public float flashDuration;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent.enabled)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private void Update()
    {
        if (agent.enabled)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    public void TakeDamage()
    {
        /*
        Material[] _mats = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;

        for (int i = 0; i < _mats.Length; i++)
        {
            _mats[i] = hitMaterial;
        }
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials = _mats;

        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
        */
        GetComponent<MeshRenderer>().material = hitMaterial;
        StartCoroutine(ResetMaterial());
        myHealth--;

        if(myHealth <= 0)
        {
            //GetComponent<Animator>().SetTrigger("isDead");
            transform.position = new Vector3(10000, 10000, 10000);
            agent.enabled = false;
            GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

            foreach (var p in _players) //fix this later
            {
                p.GetComponent<Player>().currentEnemy = null;
            }
            StopAllCoroutines();
            EnemySpawner.Instance.SpawnEnemy();
        }
        agent.enabled = false;
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);
        /*
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = defaultMaterial;
        Material[] _mats = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials;


        for (int i = 0; i < _mats.Length; i++)
        {
            _mats[i] = defaultMaterial;
        }
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials = _mats;
        */
        GetComponent<MeshRenderer>().material = defaultMaterial;
        agent.enabled = true;
    }
}
