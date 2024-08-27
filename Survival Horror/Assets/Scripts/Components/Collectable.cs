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

    public Items item;

    public Interaction player_interaction;

    private bool _isInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(item.GetType());
            //Debug.Log("entered ammo pickup range");

            player_interaction.interact(item);
            Destroy(this.gameObject);
        }
    }

    //public enum Items { thing }
}
