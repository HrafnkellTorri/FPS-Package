using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Pumkin_grenade_Explode : NetworkBehaviour
{
    public Rigidbody rb;
    public Rigidbody child;
    public GameObject pin;

    public float throwingPower = 10f;
    public float radius = 1600f;
    public float power = 10000f;
    public float Delay = 3f;
    [SyncVar]
    public bool explodeNow;

    public Transform body;
    public ParticleSystem explosion;
    public AudioSource audioDriver;
    public AudioClip explosion_Sound_FX;
    public AudioClip pin_Sound_FX;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Explosion());
        child = pin.GetComponent<Rigidbody>();
        audioDriver.GetComponent<AudioSource>();
        audioDriver.PlayOneShot(pin_Sound_FX);
        rb.AddForce(transform.forward * throwingPower * 1.5f, ForceMode.VelocityChange);
        rb.AddTorque(4, 0, 0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (explodeNow)
        {
            Boom();
        }
    }

    IEnumerator Explosion()
    {
        pin.transform.parent = null;
        //child.AddForce(transform.forward * 2, ForceMode.Impulse);
        //child.AddTorque(1, 2, 3);
        yield return new WaitForSeconds(Delay);
        if (!explodeNow)
        Boom();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }

    void Boom()
    {
        explosion.Play();
        explosion.transform.position = body.transform.position;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Player_Movement pm = hit.GetComponent<Player_Movement>();
            zombieLikeAI zi = hit.GetComponent<zombieLikeAI>();
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (zi != null)
            {
                zi.Alive = false;
                rb.AddForce(transform.forward * 500f);
                rb.AddTorque(-transform.forward * 500f);
            }
            else if (pm != null)
            {
                pm.beenKilledByPlayer = true;
            }

        }
        audioDriver.PlayOneShot(explosion_Sound_FX, 0.8f);
        Destroy(gameObject, 2.7f);
        Destroy(this);
    }
}