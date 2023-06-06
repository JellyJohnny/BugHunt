using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoveIndicator : MonoBehaviour
{
    DecalProjector decalProjector;
    float scale;
    public float fadeDuration;
    public Vector3 size;


    private void Start()
    {
        decalProjector = GetComponent<DecalProjector>();
        decalProjector.size = size;
    }

    private void OnEnable()
    {
        size = new Vector3(0.5f, 0.5f,1f);
        decalProjector = GetComponent<DecalProjector>();
        decalProjector.size = size;
    }

    private void Update()
    {

        if (size != new Vector3(0, 0, 1))
        {
            size = new Vector3(Mathf.Lerp(size.x,0,fadeDuration * Time.deltaTime), Mathf.Lerp(size.y, 0, fadeDuration * Time.deltaTime), 1); 
        }
        else
        {
            GetComponent<MoveIndicator>().enabled = false;
        }

        decalProjector.size = size;
    }
}
