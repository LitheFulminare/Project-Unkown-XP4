using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInteractable : MonoBehaviour
{
    [SerializeField] private InteractableSO interactableSO;

    public bool puzzleComplete = false;

    private Interaction player_interaction;

    private void Start()
    {
        player_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
    }

    public bool CheckIfTimeMatches()
    {
        //if (neededItem != currentItem) return false;
        Debug.Log("Checking the time");
        return true;
    }

    // called when the player presses 'F' near the puzzle
    public void Interact()
    {
        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject;

        if (!puzzleComplete)
        {
            // bust puzzle shows a regular interaction screen
            // clock will show a fullscreen image of the clock

            //string description;
            //string prompt;

            //if (!hasItemPlaced)
            //{
            //    description = interactionTextSO.description;
            //    prompt = interactionTextSO.prompt;
            //}
            //else
            //{
            //    description = "The " + $"{currentItem.inventoryName} " + interactionTextSO.altDescription;
            //    prompt = interactionTextSO.altPrompt;
            //}

            //OverlayController.interactOverlay(gameObject, neededItem, description, prompt, hasItemPlaced, currentItem);
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
        DataPersistency.addPuzzle(gameObject.name); // marks this puzzle as complete, so it won't reset when the scene reloads
    }
}
