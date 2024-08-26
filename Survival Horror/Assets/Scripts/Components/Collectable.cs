using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour
{
    // video about enums
    // https://learn.unity.com/tutorial/enumerations#
    // https://www.youtube.com/watch?v=G4Qmy2sabpo

    //public int thing = 0;

    public string[] ItemType = {"pistol ammo", "healing", "puzzle item"};

    public Interaction player_interaction;

    private bool _isInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player collided");

            /* player_interaction.interact();
            Destroy(this.gameObject); */
        }
    }
}
