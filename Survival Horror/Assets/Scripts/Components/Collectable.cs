using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed);
    public static ConfirmAction confirm;

    // video about enums
    // https://learn.unity.com/tutorial/enumerations#
    // https://www.youtube.com/watch?v=G4Qmy2sabpo

    public Items item;

    public Interaction player_interaction;


    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    public void Interact()
    {
        //Debug.Log(item); this would print pistolAmmo
        OverlayController.showUI();
        //player_interaction.interact(item); // this will be executed somewhere else
        //Destroy(this.gameObject);
    }

    public void checkIfPlayerConfirmed(bool confirm)
    {
        //Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
        if (confirm)
        {
            //player_interaction.interact(item);
            InventoryController.itemReceiver(item);
            Destroy(gameObject);  
        }

        Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
    }
}
