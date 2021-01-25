using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverItems : MonoBehaviour
{
    public float xRotate = 0;
    public float yRotate = 0;
    public float zRotate = 0;
    public float xPos = 0;
    public float yPos = 0;
    public float zPos = 0;
    public float yTravel = 0;
    public bool up = true;
    public float hoverTime = 180;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(xRotate, yRotate, zRotate));
        if (yTravel < hoverTime && up == true) {
            gameObject.transform.position += new Vector3(xPos, yPos, zPos);
            yTravel += 1;
            if (yTravel == hoverTime) {
                up = false;
            }
        }
        else {
            gameObject.transform.position -= new Vector3(xPos, yPos, zPos);
            yTravel -= 1;
            if (yTravel == 0) {
                up = true;
            }
        }
    }
}
