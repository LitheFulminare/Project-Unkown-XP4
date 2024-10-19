using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustInteractable : MonoBehaviour, IInteractable
{
    //public Items item;

    [SerializeField] CollectableSO neededItem;
    [SerializeField] InteractableSO interactionText;

    public bool puzzleComplete = false;

    public bool hasItemPlaced = false;

    private Interaction player_interaction;

    private void Start()
    {
        player_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
    }

    // called when the player presses 'F' near the item
    public void Interact()
    {
        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject;

        if (!puzzleComplete)
        {
            Debug.Log($"interaction happened with {gameObject}");

            // store the 'currentBust' as a 'CollectableSO'
            // pass 'currentBust.ingameName' or something as a parameter
            // maybe change the function to overload 'description' and 'prompt', and decide here whether the regular or alt texts should be used
            OverlayController.interactOverlay(gameObject, neededItem, interactionText, hasItemPlaced);
        }
        else
        {
            // makes the player interact with the object bound to the puzzle
            //boundObject.Use();
        }
    }

    // currently being called only by "InventoryController" and "PuzzleManager"
    // sets the puzzle as "complete" and wont interact with this anymore
    // ex.: the player will interact with the door instead of interacting with the door puzzle
    public void SetCompleted()
    {
        puzzleComplete = true;
        PuzzleManager.AddCompletedPuzzle(gameObject.name); // marks this puzzle as complete, so it won't reset when the scene reloads
    }
}
