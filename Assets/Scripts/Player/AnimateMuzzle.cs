using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMuzzle : MonoBehaviour
{
    public GameObject muzzleParent;
    public void EnableMuzzle()
    {
        muzzleParent.SetActive(true);
    }

    public void DisableMuzzle()
    {
        muzzleParent.SetActive(false);
    }
}
