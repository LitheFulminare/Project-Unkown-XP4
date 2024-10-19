using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustInteractable : MonoBehaviour, IInteractable
{
    //public Items item;

    [SerializeField] CollectableSO neededItem;

    public bool puzzleComplete = false;

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
            OverlayController.interactOverlay(gameObject, neededItem);
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
