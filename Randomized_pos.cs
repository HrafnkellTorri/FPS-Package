using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomized_pos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(Random.Range(-4,4) , Random.Range(-4, 4), 0); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
