using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Recoil_2 : NetworkBehaviour
{
    public float recoil = 150.0f;
    public float maxRecoil_x = -20;
    public float maxRecoil_y = 20f;
    public float recoilSpeed = 0.1f;

    
    public void StartRecoil(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
    {
       
        // in seconds
        recoil = recoilParam;
        maxRecoil_x = maxRecoil_xParam;
        recoilSpeed = recoilSpeedParam;
        maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
    }

    void recoiling()
    {
        if (recoil > 0f)
        {

            Quaternion maxRecoil = Quaternion.Euler(-maxRecoil_x, maxRecoil_y, 0.1f);
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed * 8);
            recoil -= Time.deltaTime * 3;
        }
        else
        {
            recoil = 0f;
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed * 12);
        }
    }

    // Update is called once per frame
    void Update()
    {
        recoiling();
    }
}
