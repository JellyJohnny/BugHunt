using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<OverAnimation>().UpdateBool("isWalking", false);
            other.gameObject.GetComponent<OverAnimation>().UpdateBool("isIdle", true);
        }
    }
}
