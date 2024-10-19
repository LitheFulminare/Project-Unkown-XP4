using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomState
{
    unexplored = 0,
    partiallyExplored = 1,
    explored = 2
}

public class RoomManager : MonoBehaviour
{
    // this should be saved to world vars so it can be accessed everywhere
    public RoomState roomState;

    private Collectable[] collectables;
    private Interactable[] interactables;

    private void Start()
    {
        // I heard that 'FindObjectsByTag' is faster, should test it later
        collectables = GameObject.FindObjectsOfType<Collectable>();
        interactables = GameObject.FindObjectsOfType<Interactable>();
    }

    // called by the 'WorldVars' class on start, after loading the scene data
    public void InitializeRoomState()
    {
        PlayerAction();

        if (roomState != RoomState.explored)
        {
            roomState = RoomState.partiallyExplored;
            //Debug.Log($"{SceneManager.GetActiveScene().name} state: {roomState}");
        }
    }

    // called when the player collects an item or completes a puzzle
    // checks if there's anything left in the room to change the 'roomState'
    public void PlayerAction()
    {
        bool allItemsCollected = true;
        bool allPuzzlesCompleted = true;

        //Debug.Log("Player interacted with world");

        // searches for every collectable
        foreach (Collectable collectable in collectables)
        {
            // if any of them is still uncollected (not destroyed), 'allItemsCollected' is false
            if (!collectable.isDestroyed)
            {
                Debug.Log($"Found uncollected item: {collectable.gameObject.name}");
                allItemsCollected = false;
            }
        }

        // searches for every interactable 
        foreach (Interactable interactable in interactables)
        {
            // if any of them is still uncompleted, 'allPuzzlesCompleted' is false
            if (!interactable.puzzleComplete)
            {
                Debug.Log($"Found unsolved puzzle: {interactable.gameObject.name}");
                allPuzzlesCompleted = false;
            }
        }

        // if all items were collected and all puzzles were solved, the room is completely explored
        if (allItemsCollected && allPuzzlesCompleted)
        {
            roomState = RoomState.explored;
            //Debug.Log("All all items collected and all puzzles solved");
        }
        //Debug.Log($"{SceneManager.GetActiveScene().name} state: {roomState}");
    }
}
