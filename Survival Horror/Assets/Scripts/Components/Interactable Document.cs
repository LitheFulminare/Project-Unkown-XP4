using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDocument : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
    public static ConfirmAction confirm;

    //[SerializeField] CollectableSO neededItem;
    //[SerializeField] Door door; // change this according to need

    [SerializeField] DocumentSO documentSO;

    [SerializeField] GameObject documentInspectorObj;
    //private DocumentInspector documentInspector;

    //public bool puzzleComplete = false;

    public Interaction player_interaction;

    private void Start()
    {
        player_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();

        confirm = checkIfPlayerConfirmed;
    }

    // called when the player presses 'F' near the document
    public void Interact()
    {
        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject;

        if (documentInspectorObj != null) Instantiate(documentInspectorObj);
        DocumentInspector.setDocument(documentSO);
        if (documentSO.isCollectable) PlayerVars.AddDocument(documentSO);
        //documentInspector.function(documentSO);
        // 

        //if (!puzzleComplete)
        //{
        //    Debug.Log("Using old interactable system"); // i'm trying to rewrite the interaction system

        //    //OverlayController.interactOverlay(gameObject, neededItem);
        //}
        //else
        //{
        //    // makes the player interact with door
        //    door.Use();
        //}
    }

    // currently being called only by "InventoryController" and "PuzzleManager"
    // sets the puzzle as "complete" and wont interact with this anymore
    // ex.: the player will interact with the door instead of interacting with the door puzzle
    //public void SetCompleted()
    //{
    //    puzzleComplete = true;
    //    DataPersistency.addPuzzle(gameObject.name); // marks this puzzle as complete, so it won't reset when the scene reloads
    //}

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
