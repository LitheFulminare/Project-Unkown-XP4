using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bust
{
    bear = 0,
    bull = 1,
    goat = 2,
    horse = 3,
    monkey = 4
}

public class BustInteractable : MonoBehaviour, IInteractable
{
    //public Items item;

    [SerializeField] CollectableSO neededItem;
    [SerializeField] InteractableSO interactionTextSO;

    public bool puzzleComplete = false;

    public bool hasItemPlaced = false;

    private Interaction player_interaction;

    private CollectableSO currentItem;

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

            string description;
            string prompt;

            if (!hasItemPlaced )
            {
                description = interactionTextSO.description;
                prompt = interactionTextSO.prompt;
            }
            else
            {
                description = interactionTextSO.altDescription + $"{currentItem.ingameName}";
                prompt = interactionTextSO.altPrompt;
            }

            OverlayController.interactOverlay(gameObject, neededItem, description, prompt);
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

    public void UsedItem(CollectableSO item)
    {
        //Instantiate(item.inspectModel, );
    }
}
