using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System;

public class Pistol_Fire : MonoBehaviour
{
    public NetworkSpawner playerNetwork;
    public Player_Movement player;
    public Recoil_2 recoil;
    [Header("Recoil Values")]
    public float recoilVal;
    public float maxRecoil;
    public GameObject muzzle;
    public GameObject gunProjectile;
    public GameObject bulletCasing;
    public GameObject newBulletToSpawn;
    public Transform chamberLocation;
    public Transform jacketLocation;
    public ParticleSystem mussleFlash;
    public AudioSource audioClip;
    public TextMeshProUGUI tmpro;
    public Animator anim;
    public float maxAmmoInMagazine = 8;
    public float ammoInMagazine = 7;
    public float reloadSpeed = 0.7f;
    public bool aim = false;
    public bool allowfiring = true;


    

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();
        tmpro = GameObject.Find("Ammo_Counter").GetComponent<TextMeshProUGUI>();
        tmpro.text = ammoInMagazine.ToString();

        
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoInMagazine > 0)
        {
            magazine();
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
            anim.Play("Empty_9MM");
        }

    }

    void aimDownSight()
    {
        if (aim == false) {
            anim.Play("Aim_Down_Sight");
            anim.Play("LeftArm_Aim_AQ8");
            aim = true;
        }

        else {
            aim = false;
            anim.Play("Aim_From_Sight");
            anim.Play("LeftArm_Aim_From_AQ8");
        }
    }

    IEnumerator ShootTimerCoroutine()
    {  
        playerNetwork.SpawnBullets(0,jacketLocation.transform.position, jacketLocation.transform.rotation);
        ammoInMagazine = ammoInMagazine - 1;
        tmpro.text = ammoInMagazine.ToString();
        Instantiate(bulletCasing, chamberLocation.transform.position, jacketLocation.transform.rotation); //Jacket spawned next to chamber
        mussleFlash.Play();
        audioClip.Play();
        anim.Play("Shoot_9MM");
        recoil.StartRecoil(recoilVal, maxRecoil, 1.10f);
        if (ammoInMagazine == 0)
        {
            anim.Play("Empty_9MM");
        }
        allowfiring = false;
        yield return new WaitForSeconds(0.1f);
        allowfiring = true;
    }

    IEnumerator ReloadTimerCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        if(aim)
        anim.Play("Reload_9MM");
        else
        anim.Play("Reload_ADS_9MM");
        allowfiring = false;
        if (ammoInMagazine == 0)
        {
            yield return new WaitForSeconds(0.8f);
            anim.Play("LockChamber_9MM");
            yield return new WaitForSeconds(reloadSpeed - 0.4f);
            ammoInMagazine = 7;
        }
        else
        {
            ammoInMagazine = 8;
            yield return new WaitForSeconds(reloadSpeed);
        }
        tmpro.text = ammoInMagazine.ToString();
        allowfiring = true;
    }



}
