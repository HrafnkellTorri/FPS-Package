using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour
{

    [Space]
    [Header("Networking")]
    public GameObject[] Bullet = new GameObject[2];
    public GameObject[] Grenades = new GameObject[2];
    public GameObject[] enemies = new GameObject[2];
    Vector3 spawnPoint;
    Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnGrenade(int id, Vector3 spawnloc, Quaternion rotation)
    {
        Cmd_SpawnGrenadesOnServer(0, spawnloc, rotation);
    }

    public void SpawnBullets(int bulletid, Vector3 spawn, Quaternion rotation)
    {
        //ClientScene.RegisterPrefab(Bullet);
        spawnPoint = spawn;
        spawnRotation = rotation;
        Cmd_SpawnBulletsOnServer(bulletid, spawn, spawnRotation);
    }

    public void SpawnEnemies(int id, Vector3 spawn)
    {
        ClientScene.RegisterPrefab(enemies[id]);
        spawnPoint = spawn;
        Cmd_spawnFoesOnServer(id, spawn);
    }

    [Command]
    public void Cmd_SpawnBulletsOnServer(int bulletId, Vector3 spawnloc, Quaternion roationplayer)
    {
        GameObject bullet = Instantiate(Bullet[bulletId], spawnloc, roationplayer);
        NetworkServer.Spawn(bullet);
    }

    [Command]
    public void Cmd_SpawnGrenadesOnServer(int prefabId, Vector3 spawnvec, Quaternion rotation)
    {
        GameObject Grenade = Instantiate(Grenades[prefabId], spawnvec, rotation);
        NetworkServer.Spawn(Grenade);
    }

    [Command]
    public void Cmd_spawnFoesOnServer(int id, Vector3 loc)
    {
        GameObject SpawnThis = Instantiate(enemies[id], loc, Quaternion.identity);
        NetworkServer.Spawn(SpawnThis);
    }

}
