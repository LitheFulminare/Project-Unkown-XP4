using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //public void UpdatePosition(Vector3 spawnPosition)
    //{
    //    gameObject.transform.position = spawnPosition;
    //}

    private void Start()
    {
        if (PlayerVars.spawnPosition != Vector3.zero )
        {
            transform.position = PlayerVars.spawnPosition;
            //Debug.Log($"Spawn position: {PlayerVars.spawnPosition}");
            //Debug.Log($"Player position: {transform.position}");
        }   
    }  
}
