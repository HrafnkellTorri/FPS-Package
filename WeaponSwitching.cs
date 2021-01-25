using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class WeaponSwitching : NetworkBehaviour
{
    public GameObject sideArm;
    public GameObject primaryGun;
    public GameObject currentGun;
    public Transform gunLoc;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim.GetComponent<Animator>();
        sideArm.SetActive(false);
        primaryGun.SetActive(true);
        currentGun = primaryGun;
        anim.Play("Support_AQ8");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) //Main Gun
        {
            print("1");
            if (currentGun != primaryGun)
            {
                anim.Play("AimFromRifle");

                sideArm.SetActive(false);
                primaryGun.SetActive(true);
                currentGun = primaryGun;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //Side Arm
        {
            anim.Play("Idle");
            print("2");
            if (currentGun != sideArm)
            {
                primaryGun.SetActive(false);
                sideArm.SetActive(true);
                currentGun = sideArm;
                
            }
        }
    }


}
