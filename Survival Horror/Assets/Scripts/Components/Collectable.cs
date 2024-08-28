using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour, IInteractable
{
    // video about enums
    // https://learn.unity.com/tutorial/enumerations#
    // https://www.youtube.com/watch?v=G4Qmy2sabpo

    public Items item;

    public Interaction player_interaction;

    private bool _isInside = false;

    public void Interact()
    {
        //Debug.Log(item); this would print pistolAmmo
        player_interaction.interact(item);
        Destroy(this.gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        //Debug.Log(item.GetType());
    //        //Debug.Log("entered ammo pickup range");

    //        player_interaction.interact(item);
    //        Destroy(this.gameObject);
    //    }
    //}

    //public enum Items { thing }
}
