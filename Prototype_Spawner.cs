using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror;
using UnityEngine;


public class Prototype_Spawner : NetworkBehaviour
{

    public GameObject enemy_1;
    public GameObject enemy_2;
    public GameObject smallMedicBox;
    public TextMeshProUGUI tmpro;
    public NetworkSpawner authoritySpawner;

    [SerializeField]
    private float spawnX;
    [SerializeField]
    private float spawnY;
    [SerializeField]
    private float spawnZ;

    [SyncVar]
    public Transform playerloc;

    public bool victory = false;
    public float maxSpawns = 100;
    public float enemiesSpawned = 0;
    public float enemiesDead = 0;
    public float timeBetweenSpawns = 5.1f;

    void Awake()
    {

        try
        {
            if (playerloc = GameObject.FindGameObjectWithTag("Player").transform)
            {

            }
        }
        catch
        {

        }

        InvokeRepeating("SpawnFoes", timeBetweenSpawns, timeBetweenSpawns);
        InvokeRepeating("SpawnItems", 0f, 5f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        try
        {
            if (playerloc = GameObject.FindGameObjectWithTag("Player").transform)
            {
                authoritySpawner =  GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkSpawner>();
            }
        }
        catch
        {
            return;
        }

        

        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] deadenemies = GameObject.FindGameObjectsWithTag("Dead");
        if (enemiesDead > enemiesSpawned)
        {
            Victory();
        }
        else
        {
            float totalEnemiesLeft = (enemiesSpawned - enemiesDead);
            tmpro.text = totalEnemiesLeft.ToString() + " Enemies Remaining";
            Victory();
        }
    }

    void SpawnFoes()
    {
        if (enemiesSpawned < maxSpawns)
        {

            if (timeBetweenSpawns > 1.4f)
            {
                timeBetweenSpawns -= 0.2f;
            }
            float enemyPicker = Random.Range(0, 100);
            if (enemyPicker <= 94) // 95 % chance of standard ghost
            {
                authoritySpawner.SpawnEnemies(0, transform.position + new Vector3(Random.Range(spawnX, -spawnX), spawnY, Random.Range(spawnZ, spawnZ)));
                //Instantiate(enemy_1, new Vector3(Random.Range(-114* 0.9f, 114* 0.9f), 2, Random.Range(-147* 0.9f, 147* 0.9f)), Quaternion.identity);
            }
            else // 5 % Chance of fast ghost
            {
                authoritySpawner.SpawnEnemies(1, transform.position + new Vector3(Random.Range(spawnX, -spawnX), spawnY, Random.Range(spawnZ, spawnZ)));
                //Instantiate(enemy_2, new Vector3(Random.Range(-114 * 0.9f, 114* 0.9f* 0.9f), 2, Random.Range(-147* 0.9f, 147* 0.9f)), Quaternion.identity);
            }
            enemiesSpawned++;
        }
    }


    void SpawnItems() {
        Instantiate(smallMedicBox, new Vector3(Random.Range(-114 * 0.9f, 114 * 0.9f), 4.5f, Random.Range(-147 * 0.9f, 147 * 0.9f)), Quaternion.identity);
    }

    void Victory()
    {
        GameObject[] deadEnemies = GameObject.FindGameObjectsWithTag("Dead");
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (aliveEnemies.Length == 0)
        {
            tmpro.text = "VICTORY!!!";
        }
    }
}
