using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TankMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=OEMk8xVDk2I

    private CharacterController controller;

    public float speed = 250;
    public float turnSpeed = 180f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // handles the movement
        // currently this can be stopped by UI elements
        if (!PlayerVars.playerBlocked)
        {
            Vector3 moveDir;

            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            moveDir = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

            controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
        }
    }
}
