using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour
{
    public Pistol_Fire pistol;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse3) || (Input.GetKeyDown(KeyCode.F))) //Punching
        {
            StartCoroutine(Punch());
        }
    }

    IEnumerator Punch()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        anim.Play("Punch");
        RaycastHit hit;
        float theDistance = 6;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * theDistance;
        Vector3 left = transform.TransformDirection(Vector3.forward - Vector3.right) * theDistance;
        Vector3 farLeft = transform.TransformDirection(Vector3.forward - Vector3.right * 2) * theDistance;
        Vector3 right = transform.TransformDirection(Vector3.forward - Vector3.left) * theDistance;
        Vector3 farRight = transform.TransformDirection(Vector3.forward - Vector3.left * 2) * theDistance;

        Debug.DrawRay(transform.position, forward, Color.red);
        Debug.DrawRay(transform.position, forward, Color.red);
        Debug.DrawRay(transform.position, forward - Vector3.right * 2, Color.red);
        Debug.DrawRay(transform.position, forward - Vector3.left, Color.red);
        Debug.DrawRay(transform.position, forward - Vector3.right, Color.red);
        Debug.DrawRay(transform.position, forward - Vector3.left * 2, Color.red);


        if (Physics.Raycast(transform.position, (forward) ,out hit, theDistance))
        {
            yield return new WaitForSeconds(0.2f);
            theDistance = hit.distance;
            Debug.Log(theDistance + " " + hit.collider.gameObject.name);
            Vector3 force = 1000f * Time.deltaTime * transform.forward;
            hit.rigidbody.AddForce((hit.transform.position - transform.position) * 400, ForceMode.Acceleration);
        }
        if (Physics.Raycast(transform.position, (right), out hit, theDistance))
        {
            yield return new WaitForSeconds(0.2f);
            theDistance = hit.distance;
            Debug.Log(theDistance + " " + hit.collider.gameObject.name);
            Vector3 force = 1000f * Time.deltaTime * transform.forward;
            try
            {
                hit.rigidbody.AddForce((hit.transform.position - transform.position) * 400, ForceMode.Acceleration);
            }
            catch
            {

            }
        }
        if (Physics.Raycast(transform.position, (farRight), out hit, theDistance))
        {
            yield return new WaitForSeconds(0.2f);
            theDistance = hit.distance;
            Debug.Log(theDistance + " " + hit.collider.gameObject.name);
            Vector3 force = 1000f * Time.deltaTime * transform.forward;
            try
            {
                hit.rigidbody.AddForce((hit.transform.position - transform.position) * 400, ForceMode.Acceleration);
            }
            catch
            {

            }
        }
        if (Physics.Raycast(transform.position, (farLeft), out hit, theDistance))
        {
            yield return new WaitForSeconds(0.2f);
            theDistance = hit.distance;
            Debug.Log(theDistance + " " + hit.collider.gameObject.name);
            Vector3 force = 1000f * Time.deltaTime * transform.forward;
            try
            {
                hit.rigidbody.AddForce((hit.transform.position - transform.position) * 400, ForceMode.Acceleration);
            }
            catch
            {

            }
        }
        if (Physics.Raycast(transform.position, (left), out hit, theDistance))
        {
            yield return new WaitForSeconds(0.2f);
            theDistance = hit.distance;
            Debug.Log(theDistance + " " + hit.collider.gameObject.name);
            Vector3 force = 1000f * Time.deltaTime * transform.forward;
            try
            {
                hit.rigidbody.AddForce((hit.transform.position - transform.position) * 400, ForceMode.Acceleration);
            }
            catch
            {

            }
        }

        //yield return new WaitForSeconds(1 - 0.2f); Delay --- Check this later???
        //pistol.allowfiring = true;
    }
}
