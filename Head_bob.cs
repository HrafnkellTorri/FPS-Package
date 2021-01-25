using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_bob : MonoBehaviour
{
    public Pistol_Fire playerScript;
    public Player_Movement playerMovements;
    private float timer = 0.1f;
    float bobbingSpeed = 0.06f;
    float bobbingAmount = 0.04f;
    float midpoint = 0.84f;

    private void Start()
    {
        playerScript.GetComponent<Player_Movement>();
    }

    void Update()
    {

        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            if(!playerScript.aim == true && playerMovements.isGrounded && playerMovements.sprintKey)
            {
                translateChange = 0;
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            }
            else if (!playerScript.aim == true)
            {
                translateChange = 0;
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 2.0f);
            }
            else
            {
                translateChange = 0;
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 0.1f);
            }

            translateChange = totalAxes * translateChange;
            cSharpConversion.y = midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = midpoint;
        }

        transform.localPosition = cSharpConversion;
    }
}
