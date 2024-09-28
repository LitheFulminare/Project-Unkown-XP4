using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour // this will mainly keep track of completed puzzles and set them as complete
{
    // keeps track of completed puzzles
    // gets updated by 'WorldVars' on start
    public static List<string> completedPuzzles = new List<string>();

    // Called by 'WorldVars'
    public static void CheckCompletePuzzles()
    {
        // gets every puzzle that was completed previously and sets it as complete when the scene loads
        foreach (var name in completedPuzzles)
        {
            GameObject item = GameObject.Find(name);
            if (item != null) { item.SendMessage("SetCompleted"); }
        }
    }

    // called by 'Collectable' in 'Collected()', receives the gameObject name
    public static void AddDestroyedItem(string name)
    {
        //destroyedItems.Add(name);
    }

    // called by "SceneChanger" before loading the scene
    // sends what items were destroyed to PlayerVars to make the data persistent
    public void SaveList()
    {
        //WorldVars.SaveDestroyedItems(destroyedItems, SceneManager.GetActiveScene().name);
    }
}
