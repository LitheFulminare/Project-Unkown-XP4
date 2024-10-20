using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private CollectableSO neededItem;
    [SerializeField] private InteractableSO interactionTextSO;

    [SerializeField] private GameObject spawnPosObj;

    public bool puzzleComplete = false;

    public bool hasItemPlaced = false;

    private Interaction player_interaction;

    public CollectableSO currentItem;

    GameObject bust;

private void Start()
    {
        player_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
    }

    public bool CheckIfItemsMatch()
    {
        if (neededItem == currentItem)
        {
            return true;
        }

        return false;
    }

    // called when the player presses 'F' near the item
    public void Interact()
    {
        // keeps track of what the player is currently interacting with
        Manager.currentInteractionObj = gameObject;

        if (!puzzleComplete)
        {
            string description;
            string prompt;

            if (!hasItemPlaced )
            {
                description = interactionTextSO.description;
                prompt = interactionTextSO.prompt;
            }
            else
            {
                description = "The " + $"{currentItem.inventoryName} " + interactionTextSO.altDescription;
                prompt = interactionTextSO.altPrompt;
            }

            OverlayController.interactOverlay(gameObject, neededItem, description, prompt, hasItemPlaced, currentItem);
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

    // called when the player selects an item on the inventory
    // also called on start by BustPuzzleManager to sync data from previous session
    public void UsedItem(CollectableSO item)
    {
        if (item != null)
        {
            currentItem = item;

            hasItemPlaced = true;

            bust = Instantiate(item.inspectModel);

            bust.transform.position = spawnPosObj.transform.position;
            bust.transform.localScale = spawnPosObj.transform.localScale;
            bust.transform.rotation = spawnPosObj.transform.rotation;

            // calls the manager to check whether the puzzle is complete or not
            BustPuzzleManager.placeBust(this, currentItem);
        }

        else
        {
            // destroy the item on start if it was previouly removed
            ItemRetrieved();
        }
    }

    private void ItemRetrieved()
    {
        if (bust != null)
        {
            Destroy(bust);
            hasItemPlaced = false;

            // sets the current item as null so it wont respawn afer reloading the scene
            BustPuzzleManager.removeBust(this, currentItem);
        }
    }
}
