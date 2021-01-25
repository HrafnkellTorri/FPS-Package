using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player_Movement : NetworkBehaviour
{

    public CharacterController controller;

    public Camera cam;
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;
    public float movementSpeed = 20f;
    public float sprintSpeed = 1.5f;
    public GameObject respawn;
    public float jumpheight = 30f;

    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;

    public bool sprintKey;

    Vector3 velocity;
    public bool isGrounded;

    Vector3 move;

    public bool beenKilledByPlayer = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }

        checkIfFloorIsLava();

        float walkingSpeed = movementSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);




        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprintKey = true;
                walkingSpeed = movementSpeed * sprintSpeed;
            }
            else
            {
                sprintKey = false;
            }

            if (x > 0 && z > 0 || x < 0 && z < 0 || x > 0 && z < 0 || x < 0 && z > 0)
            {
                walkingSpeed = movementSpeed / 1.4f;
            }
            velocity.y = 0f;
            //if (Input.GetButtonDown("a") || (Input.GetButtonDown("w")) || (Input.GetButtonDown("d")) || (Input.GetButtonDown("s")))
            //{
                move = transform.right * x + transform.forward * z;
            //}

        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (-Vector3.up), out hit, 30))
        {
            float angleoffloor = Vector3.Angle(-transform.up, hit.transform.forward);
        }

        controller.Move(move * walkingSpeed * Time.deltaTime);

        float armReach = 2;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * armReach * 1f;
        Vector3 up = transform.TransformDirection(Vector3.forward - Vector3.up) * armReach * 0.5f;
        Vector3 down = transform.TransformDirection(Vector3.forward - Vector3.down * 0.7f) * armReach * 1f;

        if (Input.GetButtonDown("Jump") && isGrounded || Input.GetButtonDown("Jump") && Physics.CheckSphere(groundCheck.position, groundDistance * 2.5f, groundMask))
        {
            velocity.y = Mathf.Sqrt(jumpheight * 4f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && ((Physics.Raycast(transform.position, (forward), out hit, armReach * 1f) || Physics.Raycast(transform.position, (up), out hit, armReach * 0.5f) || Physics.Raycast(transform.position, (down), out hit, armReach * 1f)) && hit.transform.gameObject.tag == "Wall"))
        {
            float angle = Vector3.Angle(-transform.up, hit.transform.forward);
            Debug.Log(angle);
            float rotSpeed = 0.45f;
            velocity.y = Mathf.Sqrt(jumpheight * 4f * gravity);
            float dirSelector = Random.Range(0, 100);
            Debug.Log(dirSelector.ToString());


            if (dirSelector >= 50f) //Rotates right
            {
                StartCoroutine(Rotate(Vector3.up, 180, rotSpeed));
            }
            else //Rotates left
            {
                StartCoroutine(Rotate(Vector3.up, -180, rotSpeed));
            }
            move = -move;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); //I think this is keeping velocity in air

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.tag);
            beenKilledByPlayer = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Moving_Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Moving_Platform")
        {
            Debug.Log(other.gameObject.tag);
            transform.position = transform.position + other.transform.position;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
    }

    private void checkIfFloorIsLava()
    {
        if (beenKilledByPlayer)
        {
            controller.enabled = false;
            respawn = GameObject.Find("PlayerSpawn1");
            transform.position = respawn.transform.position;
            controller.enabled = true;
            beenKilledByPlayer = false;
        }
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (-Vector3.up), out hit, 2))
        {
            if (hit.collider.transform.tag == "Lava")
            {
                Debug.DrawRay(transform.position, transform.forward, Color.cyan);
                Debug.Log(hit.collider.transform.tag);
                beenKilledByPlayer = true;
            }
        }
    }


    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }

}

    