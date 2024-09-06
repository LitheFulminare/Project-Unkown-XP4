using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
    public static ConfirmAction confirm;

    //public Items item;

    public Interaction player_interaction;


    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    public void Interact()
    {
        //OverlayController.showUI(gameObject, item); create other function to show custom text
    }

    // this probably wont be used
    public void checkIfPlayerConfirmed(bool confirm, GameObject itemChecker)
    {
        //Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
        if (confirm)// && itemChecker == gameObject)
        {
            //player_interaction.interact(item);

        }

        Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
    }

    private void selfDestruct()
    {
        gameObject.SetActive(false);
    }

    // called by ButtonAction in the Overlay Controller if the player accepts to pick up the item
    // wont be called since this function doesnt make sense here
    private void collected()
    {
        // find an empty space and adds item to the stack, then it self destructs
        InventoryController.itemReceiver(item);
        //selfDestruct(); the door isnt supposed to self destruct
    }
}
