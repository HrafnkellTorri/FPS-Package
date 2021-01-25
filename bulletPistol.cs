using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPistol : MonoBehaviour
{

    Player_Movement player;
    public float bulletRotation;
    public float bulletVelocity = 100;
    public float bulletDamage = 50;
    public bool active = true;
    public ParticleSystem bulletCollision;
    public ParticleSystem bloodSplatter;
    public bool hasInstancedBlood, hasInstancedCollision = false;
    float angle;

    // Start is called before the first frame update
    void Awake()
    {
        hitRegistration();
        gameObject.transform.rotation *= Quaternion.Euler(0, bulletRotation,0);
        //transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(active)
        hitRegistration();
        gameObject.transform.Translate(Vector3.right * bulletVelocity/2 * Time.fixedDeltaTime);
        hitRegistration();
        gameObject.transform.Translate(Vector3.right * bulletVelocity/2 * Time.fixedDeltaTime);

    }

    void hitRegistration()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.right, out hit, 8f))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log(hit.transform.name);
                float randomFallof = Random.Range(-25f, 50f);
                hit.collider.gameObject.GetComponentInParent<zombieLikeAI>().Take_Damage(bulletDamage + randomFallof);
                
                Debug.DrawRay(transform.position, transform.rotation * Vector3.right * 100f, Color.red);
                Debug.Log("Did Hit" + (bulletDamage + randomFallof));
                active = false;
                bloodSplatter.Play();

                Destroy(gameObject);
                hasInstancedBlood = true;
            }
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<Player_Movement>().beenKilledByPlayer = true;
            }
            else if (hit.transform.CompareTag("Grenade"))
            {
                hit.transform.GetComponentInParent<Pumkin_grenade_Explode>().explodeNow = true;
            }
            else if (hit.transform.gameObject.layer == 8)
            {
                Destroy(gameObject, 1f);
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            float randomFallof = Random.Range(-25f, 50f);
            collision.gameObject.GetComponentInParent<zombieLikeAI>().Take_Damage(bulletDamage + randomFallof);
            Debug.Log("Did Hit");
            active = false;
            bloodSplatter.Play();

            Destroy(gameObject, 0.2F);
            hasInstancedBlood = true;
        }
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player_Movement>().beenKilledByPlayer = true;
        }
        else if (collision.transform.CompareTag("Grenade"))
        {
            collision.transform.GetComponentInParent<Pumkin_grenade_Explode>().explodeNow = true;
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }*/
}
