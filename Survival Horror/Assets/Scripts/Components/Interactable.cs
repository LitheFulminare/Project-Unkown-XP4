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

    [SerializeField] Items neededItem;
    [SerializeField] Door door; // change this according to need
    
    public bool puzzleComplete = false;

    public Interaction player_interaction;

    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    // called when the player presses 'F' near the item
    public void Interact()
    {
        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject; 

        if (!puzzleComplete)
        {
            //Debug.Log($"interaction happened with {gameObject}");
            OverlayController.interactOverlay(gameObject, neededItem);
        }
        else
        {
            // makes the player interact with door
            door.Use();
        }
    }

    // currently being called only by "InventoryController" and "PuzzleManager"
    // sets the puzzle as "complete" and wont interact with this anymore
    // ex.: the player will interact with the door instead of interacting with the door puzzle
    public void SetCompleted()
    {
        puzzleComplete = true;
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

    // probably wont be used too
    private void selfDestruct()
    {
        gameObject.SetActive(false);
    }

    // called by ButtonAction in the Overlay Controller if the player accepts to pick up the item
    // wont be called since this function doesnt make sense here
    private void collected()
    {
        // find an empty space and adds item to the stack, then it self destructs
        //InventoryController.itemReceiver(item);
        //selfDestruct(); the door isnt supposed to self destruct
    }
}
