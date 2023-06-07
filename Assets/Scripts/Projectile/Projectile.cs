using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Projectile")
        {
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyMovement>().TakeDamage();
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
