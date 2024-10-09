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

    private bool spawned = false;

    public float speed = 250;
    public float turnSpeed = 180f;

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += SpawnOnSceneLoaded;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= SpawnOnSceneLoaded;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Spawn();
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
        else
        {
            SetSpawned();
        }
    }

    //private void Spawn()
    //{
    //    if (PlayerVars.spawnPosition != Vector3.zero)
    //    {
    //        transform.position = PlayerVars.spawnPosition;

    //        Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");

    //        Debug.Log($"Player position: {transform.position}");

    //        transform.position = PlayerVars.spawnPosition;

    //        // this line right here is causing a bug
    //        PlayerVars.BlockPlayer(false);
    //    }
    //}

    private void SpawnOnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SpawnOnSceneLoaded was called");

        if (PlayerVars.spawnPosition != Vector3.zero)
        {
            transform.position = PlayerVars.spawnPosition;

            Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");

            Debug.Log($"Player position: {transform.position}");

            transform.position = PlayerVars.spawnPosition;

            // this line right here is causing a bug
            PlayerVars.BlockPlayer(false);
        }
    }

    public void SpawnPlayer(Vector3 newPosition)
    {
        Debug.Log("PLAYER SPAWNED");

        Debug.Log($"New position: {newPosition}");
        Debug.Log($"Player position before change: {transform.position}");

        transform.position = newPosition;

        Debug.Log($"Player position after change: {transform.position}");
    }

    public void SetSpawned()
    {
        if (!spawned)
        {
            spawned = true;
            PlayerVars.BlockPlayer(false);
        }       
    }
}
