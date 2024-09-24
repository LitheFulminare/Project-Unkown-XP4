using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private void Start()
    {
        if (PlayerVars.spawnPosition != Vector3.zero )
        {
            //while ( transform.position != PlayerVars.spawnPosition)
            //{
            //    transform.position = PlayerVars.spawnPosition;
            //    Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");
            //    Debug.Log($"Player position: {transform.position}");
            //    transform.position = PlayerVars.spawnPosition;
            //}

            // sets the position to the spawn position

            transform.position = PlayerVars.spawnPosition;

            Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");

            Debug.Log($"Player position: {transform.position}");

            transform.position = PlayerVars.spawnPosition;

            // this line right here is causing a bug
            //PlayerVars.BlockMovement(false);
        }   
    }  
}
