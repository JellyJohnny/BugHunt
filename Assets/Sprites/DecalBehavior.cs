using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalBehavior : MonoBehaviour
{
    DecalProjector dp;
    float decalWidth;
    float decalHeight;
    float newWidth;
    float newHeight;

    private void Start()
    {
        dp = GetComponent<DecalProjector>();

        decalWidth = dp.size.x;
        decalHeight = dp.size.y;

        newWidth = decalWidth * 1.5f;
        newHeight = decalHeight * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float _x = dp.size.x;
        float _y = dp.size.y;
        _x = Mathf.PingPong(Time.time, 0.5f);
        _y = Mathf.PingPong(Time.time, 0.5f);

        dp.size = new Vector3(_x, _y,1f);
    }
}
