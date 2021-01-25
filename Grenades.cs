using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Grenades : NetworkBehaviour
{
    public Animator anim;
    public NetworkSpawner netSpawn;
    public GameObject PumkinGrenade;
    public Transform ThrowFrom;
    public bool canThrowGrenades = false;
    private float timeStamp;
    public float coolDownPeriodInSeconds = 5;
    public float x = 0;
    public float y = 0;
    public float z = 0;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && isLocalPlayer && timeStamp <= Time.time)
        {
            StartCoroutine(throwGrenade());
        }
    }


    IEnumerator throwGrenade() //PumkinGrenade
    {
        timeStamp = Time.time + coolDownPeriodInSeconds;
        canThrowGrenades = false;
        //anim.Play("Punch");
        yield return new WaitForSeconds(0.2f);
        Vector3 originPoint = ThrowFrom.position + new Vector3(x, y, z);
        netSpawn.SpawnGrenade(0, originPoint + transform.forward, ThrowFrom.rotation);
        //Instantiate(PumkinGrenade, originPoint + transform.forward, ThrowFrom.rotation);
        yield return new WaitForSeconds(2);
    }
}
