using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; //transfrom of FirstPersonController

    private float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //look around (move player around it's y axis and move camera around it's x axis)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //rotate player left and right
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //rotate camera up and down
        
        xRotation -= mouseY; //calculates rotation every frame
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp rotation
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotate around the camera's x axis
        playerBody.Rotate(Vector3.up * mouseX); //Vector3.up is player's y axis
    }
}
