using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float maxVolume;

    AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0f;
    }

    private void Update()
    {
        if(aud.volume >= maxVolume)
        {
            return;
        }
        else
        {
            aud.volume = Mathf.Lerp(aud.volume,maxVolume,Time.deltaTime);
        }
    }
}
