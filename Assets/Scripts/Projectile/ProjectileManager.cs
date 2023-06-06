using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public GameObject projectile;

    public float instanceDelay;

    public float projectileSpeed;

    public float spread;

    public GameObject enemy;
    public float yAimOffset;

    private void Start()
    {
        StartCoroutine(ShootProjectile());
    }

    private void OnEnable()
    {
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        //GameObject _proj = Instantiate(projectile, transform.position, ModeManager.instance.currentFirstPersonCamera.transform.rotation);

        Vector3 _rand = Random.insideUnitSphere * spread;
        Vector3 _vel = Vector3.zero;

        if (enemy != null)
        {
            _vel = (new Vector3(enemy.transform.position.x, enemy.transform.position.y + yAimOffset, enemy.transform.position.z) - transform.position) + _rand;
        }
        else
        {
            _vel = transform.forward + _rand;
        }
        //_proj.GetComponent<Rigidbody>().velocity = _vel * projectileSpeed;

        yield return new WaitForSeconds(instanceDelay);
        StartCoroutine(ShootProjectile());  
    }
}
