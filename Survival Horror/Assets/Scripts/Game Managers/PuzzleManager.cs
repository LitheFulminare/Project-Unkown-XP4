using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour // this will mainly keep track of completed puzzles and set them as complete
{
    private static RoomManager roomManager;

    // keeps track of completed puzzles
    // gets updated by 'WorldVars' on start
    public static List<string> completedPuzzles = new List<string>();

    private void Start()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    // Called by 'WorldVars'
    public static void CheckCompletePuzzles()
    {
        // gets every puzzle that was completed previously and sets it as complete when the scene loads
        foreach (var puzzleName in completedPuzzles)
        {
            GameObject puzzleObj = GameObject.Find(puzzleName);
            Interactable puzzle = puzzleObj.GetComponent<Interactable>();
            if (puzzle != null) puzzle.SetCompleted();

            //if (puzzle != null) { puzzle.SendMessage("SetCompleted"); }
        }
    }

    // called by 'Interactable' in 'SetCompleted()', receives the gameObject name
    public static void AddCompletedPuzzle(string puzzleName)
    {
        roomManager.PlayerAction();

        // because of how 'SetCompleted()' is structured, I have to check if the puzzle is not already on the list
        if (!completedPuzzles.Contains(puzzleName))
        {
            completedPuzzles.Add(puzzleName);
        }       
    }

    // called by "SceneChanger" before loading the scene
    // sends what puzzles are completed on the current scene to WorldVars to make the data persistent
    public void SaveList()
    {
        WorldVars.SaveCompletedPuzzles(completedPuzzles, SceneManager.GetActiveScene().name);
    }
}
