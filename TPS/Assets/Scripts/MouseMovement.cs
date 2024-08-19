using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 420f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;
    
    void Start()
    {
        // Locks the cursor and makes it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        // Gets mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation around the X axis (up and down)
        xRotation -= mouseY;

        // Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Rotation around the y axis (left and right);
        yRotation += mouseX;

        // Apply rotation to the transfom
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
