using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellEjection : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * 10, ForceMode.Impulse);
        Destroy(gameObject, 3);
        rb.AddTorque(transform.forward * 200, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
