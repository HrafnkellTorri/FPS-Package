using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
// Please make sure to either name the script "FollowPlayer_AI" or change that text next
// to public class \/ to the name of your script.
public class zombieLikeAI : NetworkBehaviour
{

    public GameObject protoSpawn;
    public Prototype_Spawner protoScript;
    
    [SyncVar]
    public Transform player;

    [SyncVar]
    public Vector3 syncPos;

    public Rigidbody rb;

    public ParticleSystem bloodspatter;

    [SyncVar]
    public float health = 100;

    public float rotSpeed, moveSpeed;
    [SyncVar]
    private float distance;
    [SyncVar]
    public float maxDistance; // the maximum distance the enemy will be able to "lock onto" the player from.
    [SyncVar]
    public bool Alive = true;

    void Start()
    {

        protoSpawn = GameObject.FindGameObjectWithTag("ObjectSpawner");
        protoScript = protoSpawn.GetComponent<Prototype_Spawner>();
        //syncPos.transform.position

    }

    void FixedUpdate()
    {

        //gameObject.transform.position = syncPos;

        if (!Alive)
        {
            Enemy_Death();
            rb.constraints = RigidbodyConstraints.None;
            Destroy(gameObject, 45);
        }
        else
        { 
            GameObject playerObj = FindClosestEnemy();
            if (playerObj)
            {
                player = playerObj.transform;
                FollowPlayer();
            }
        }

        //syncPos = gameObject.transform.position;

        if (health <= 0)
        {
            Alive = false;
            gameObject.tag = "Dead";
            Enemy_Death();
        }

    }

    void FollowPlayer()
    {
        ServerMoveFoe();
    }



    public void Enemy_Death()
    {
        protoScript.enemiesDead += 1f;

        rb.constraints = RigidbodyConstraints.None;
        rb.AddRelativeTorque(new Vector3(Random.Range(-1115, 1115), Random.Range(-1115, 1115), Random.Range(-1115, 1115)));
        gameObject.tag = "Dead";
        Destroy(this);
    }

    public void Take_Damage(float dmg)
    {
        bloodspatter.Play();
        health -= dmg;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    [Server]
    void ServerMoveFoe()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

}