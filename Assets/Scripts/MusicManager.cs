using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float maxVolume;

    AudioSource aud;
    public bool musicEnabled = true;
    bool loaded = false;
    public float loadDelay = 1f;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0f;
        StartCoroutine(SetLoadStatus());
    }

    IEnumerator SetLoadStatus()
    {
        yield return new WaitForSeconds(loadDelay);
        loaded = true;
    }

    private void Update()
    {
        if (loaded)
        {
            if (musicEnabled)
            {

                if (aud.volume >= maxVolume)
                {
                    return;
                }
                else
                {
                    aud.volume = Mathf.Lerp(aud.volume, maxVolume, Time.deltaTime);
                }
            }
        }
    }
}
