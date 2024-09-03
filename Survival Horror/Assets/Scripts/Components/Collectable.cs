using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
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
        OverlayController.showUI(gameObject);
    }

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

    private void collected()
    {
        InventoryController.itemReceiver(item);
        selfDestruct();
    }
}
