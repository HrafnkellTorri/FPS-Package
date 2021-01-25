using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LocalCamera : NetworkBehaviour
{

    public Camera cam;

    void Start()
    {
        if (!isLocalPlayer || !isServer)
        {
            cam.enabled = false;
        }
    }

    void Update() {

        if (!isLocalPlayer) return; // function stops here

        // your input

    }
}
