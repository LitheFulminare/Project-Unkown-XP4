using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RoomState
{
    unexplored = 0,
    partiallyExplored = 1,
    explored = 2
}

public class RoomManager : MonoBehaviour
{
    // save this in WorldVars
    public RoomState roomState;

    private Collectable[] collectables;
    private Interactable[] interactables;

    private void Start()
    {
        collectables = GameObject.FindObjectsOfType<Collectable>();
        interactables = GameObject.FindObjectsOfType<Interactable>();
    }

    // called when the player collects an item or completes a puzzle
    // checks if there's anything left in the room to change the 'roomState'
    public void PlayerAction()
    {
        bool allItemsCollected = true;
        bool allPuzzlesCompleted = true;

        // searches for every collectable
        foreach (Collectable collectable in collectables)
        {
            // if any of them is still uncollected, 'allItemsCollected' is false
            if (collectable.isActiveAndEnabled)
            {
                allItemsCollected = false;
                break;
            }
        }

        // searches for every interactable 
        foreach (Interactable interactable in interactables)
        {
            // if any of them is still uncompleted, 'allPuzzlesCompleted' is false
            if (!interactable.puzzleComplete)
            {
                allPuzzlesCompleted = false;
            }
        }

        // if all items were collected and all puzzles were solved, the room is completely explored
        if (allItemsCollected && allPuzzlesCompleted)
        {
            roomState = RoomState.explored;
        }

        Debug.Log("All all items collected and all puzzles solved");
    }
}
