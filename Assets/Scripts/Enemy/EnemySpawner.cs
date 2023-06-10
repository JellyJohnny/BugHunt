using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;    
    public GameObject enemy;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEnemy()
    {
        GameObject _newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

        GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");

        int _rand = Random.Range(0, _players.Length);

        _newEnemy.GetComponent<EnemyMovement>().target = _players[_rand].transform;
    }
}
