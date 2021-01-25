using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShotScript : MonoBehaviour
{

    public bool hasBeenHeadshotted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HEADSHOT!");

        if (collision.gameObject.tag == "Pistol_Bullet")
        {
            Debug.Log("HEADSHOT!");
            hasBeenHeadshotted = true;
        }
    }
}
