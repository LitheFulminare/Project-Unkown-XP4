using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
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

        Spawn();
    }

    void Update()
    {
        // handles the movement
        // currently this can be stopped by UI elements
        if (!PlayerVars.isMovementBlocked)
        {
            Vector3 moveDir;

            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            moveDir = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

            controller.Move(moveDir * Time.deltaTime - Vector3.up * 0.1f);
        }     
    }

    private void Spawn()
    {
        if (PlayerVars.spawnPosition != Vector3.zero)
        {
            transform.position = PlayerVars.spawnPosition;

            //Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");

            //Debug.Log($"Player position: {transform.position}");

            transform.position = PlayerVars.spawnPosition;

            // this line right here is causing a bug
            PlayerVars.BlockMovement(false);
        }
    }
}
