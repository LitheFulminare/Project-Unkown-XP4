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

            // the only way a room is Unexplored is if it's never entered
            // no need to worry about it then
        }
    }

    // called when the player collects an item or completes a puzzle
    // also called when the scene is Initialized
    // checks if there's anything left in the room to change the 'roomState'
    public void PlayerAction()
    {
        // to sum up -> if any item or unsolved puzzle is found, these bools below will be false, room state will be partially explored
        // if not, it's completely explored

        bool allItemsCollected = true;
        bool allPuzzlesCompleted = true;

        if (collectables == null || collectables.Length == 0) return;

        foreach (Collectable collectable in collectables)
        {
            if (!collectable.isDestroyed)
            {
                //Debug.Log($"Found uncollected item: {collectable.gameObject.name}");
                allItemsCollected = false;
            }
        }

        foreach (Interactable interactable in interactables)
        {
            if (!interactable.puzzleComplete)
            {
                //Debug.Log($"Found unsolved puzzle: {interactable.gameObject.name}");
                allPuzzlesCompleted = false;
            }
        }

        if (allItemsCollected && allPuzzlesCompleted)
        {
            roomState = RoomState.explored;
            //Debug.Log("All all items collected and all puzzles solved");
        }

        //Debug.Log($"{SceneManager.GetActiveScene().name} state: {roomState}");
    }
}
