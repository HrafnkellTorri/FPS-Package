using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Rotation : MonoBehaviour
{
    public Player_Movement player;
    public float mouseSensitivity = 100f;
    public Renderer meshToHide;
    public Transform playerBody;

    public float xRotation = 0f;
    public float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!player.isLocalPlayer)
        {
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isLocalPlayer)
        {
            return;
        }


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -89.8f, 89.8f);

        if (xRotation < -30f)
        {
            Debug.Log("HideBody");
            meshToHide.enabled = false;
        }
        else
        {
            meshToHide.enabled = true;
        }

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerbody.Rotate(Vector3.up * mouseX);

        mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
