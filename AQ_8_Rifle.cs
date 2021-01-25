using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System;


public class AQ_8_Rifle : MonoBehaviour
{
    public NetworkSpawner playerNetwork;
    public Player_Movement player;
    public Recoil_2 recoil;
    [Header("Recoil Values")]
    public float recoilVal;
    public float maxRecoil;
    public GameObject chaimber;
    public GameObject gunProjectile;
    public GameObject bulletCasing;
    public GameObject newBulletToSpawn;
    public Transform jacketLocation;
    public Transform chamberLocation;
    public ParticleSystem mussleFlash;
    public AudioSource audioClip;
    public TextMeshProUGUI tmpro;
    public float maxAmmoInMagazine = 8;
    public float ammoInMagazine = 7;
    public float reloadSpeed = 0.7f;
    public bool aim = false;
    public bool allowfiring = true;


    public float FireRate = 10;  // The number of bullets fired per second
    public float lastfired;


    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {

        audioClip = GetComponent<AudioSource>();
        tmpro = GameObject.Find("Ammo_Counter").GetComponent<TextMeshProUGUI>();
        tmpro.text = ammoInMagazine.ToString();
        anim.SetBool("RifleIdle", true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player.isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //aim = false;
            StartCoroutine(ReloadTimerCoroutine());
        }
        if (Input.GetKey(KeyCode.Mouse0) && ammoInMagazine > 0)
        {
            if (Time.time - lastfired > 1 / FireRate)
            {

                magazine();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            aimDownSight();
        }
    }



    void magazine()
    {
        if (ammoInMagazine > 0 && allowfiring)
        {

            StartCoroutine(ShootTimerCoroutine());
        }
        else if (allowfiring)
        {
            //anim.Play("Empty_AQ8");
        }

    }

    void aimDownSight()
    {
        if (aim == false)
        {
            //anim.SetBool("RifleAiming", true);
            anim.Play("AimRifle");      
            aim = true;
        }

        else
        {
            //anim.SetBool("RifleAiming", false);
            anim.Play("AimFromRifle");
            aim = false;
        }
    }

    IEnumerator ShootTimerCoroutine()
    {
        playerNetwork.SpawnBullets(1,jacketLocation.transform.position, jacketLocation.transform.rotation);
        ammoInMagazine = ammoInMagazine - 1;
        tmpro.text = ammoInMagazine.ToString();
        Instantiate(bulletCasing, chamberLocation.transform.position, jacketLocation.transform.rotation); //Jacket spawned next to chamber
        mussleFlash.Play();
        audioClip.Play();
        anim.Play("Chamber_AQ8", 1);
        recoil.StartRecoil(recoilVal, maxRecoil, 1.20f);
        //anim.SetBool("RifleReload", false);
        if (ammoInMagazine == 0)
        {
            //anim.Play("Empty_9MM");
        }
        allowfiring = false;

        yield return new WaitForSeconds(0.1f);
        allowfiring = true;
    }

    IEnumerator ReloadTimerCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        aim = false;
        anim.Play("Support_AQ8");
        anim.SetBool("RifleReload", true);
        allowfiring = false;
        if (ammoInMagazine == 0)
        {
            //anim.Play("LockChamber_9MM");
            yield return new WaitForSeconds(reloadSpeed);
            ammoInMagazine = maxAmmoInMagazine - 1;
        }
        else
        {
            ammoInMagazine = maxAmmoInMagazine;
            yield return new WaitForSeconds(reloadSpeed);
        }
        tmpro.text = ammoInMagazine.ToString();
        anim.SetBool("RifleReload", false);
        allowfiring = true;
    }



}
