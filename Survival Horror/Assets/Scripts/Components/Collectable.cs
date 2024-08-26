using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    public Interaction player_interaction;

    private bool _isInside = false;

    void Start()
    {
        
    }



    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player collided");
            player_interaction.interact();
        }
    }
}
