using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldVars : MonoBehaviour // serves a similar purpose to PlayerVars, it stores data like items collected and puzzles done
{
    // stores what items were destroyed
    // there is a list for each room
    private static List<string> destroyedItemsRoom1 = new List<string>();
    private static List<string> destroyedItemsRoom2 = new List<string>();
    private static List<string> destroyedItemsRoom3 = new List<string>();

    // same thing but to store what puzzles were done
    private static List<string> completedPuzzlesRoom1 = new List<string>();
    private static List<string> completedPuzzlesRoom2 = new List<string>();
    private static List<string> completedPuzzlesRoom3 = new List<string>();

    void Start()
    {
        LoadDestroyedItems();
    }
    
    // Called by 'CollectableManager' after SceneChanger calls it to save the items
    public static void SaveDestroyedItems(List<string> destroyedItems, string sceneName)
    {
        switch (sceneName)
        {
            case "Scene1": destroyedItemsRoom1.Clear(); destroyedItemsRoom1.AddRange(destroyedItems); break;
            case "Scene2": destroyedItemsRoom2.Clear(); destroyedItemsRoom2.AddRange(destroyedItems); break;
            case "Scene3": destroyedItemsRoom3.Clear(); destroyedItemsRoom3.AddRange(destroyedItems); break;
        }
    }

    public static void SaveCompletedItems(List<string> completedPuzzles, string sceneName)
    {
        switch (sceneName)
        {
            case "Scene1": completedPuzzlesRoom1.Clear(); completedPuzzlesRoom1.AddRange(completedPuzzles); break;
            case "Scene2": completedPuzzlesRoom2.Clear(); completedPuzzlesRoom2.AddRange(completedPuzzles); break;
            case "Scene3": completedPuzzlesRoom3.Clear(); completedPuzzlesRoom3.AddRange(completedPuzzles); break;
        }
    }

    private void LoadDestroyedItems()
    {
        // loads the destroyed items on the 'CollectableManager'
        CollectableManager.destroyedItems.Clear();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Scene1": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom1); break;
            case "Scene2": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom2); break;
            case "Scene3": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom3); break;
        }

        // calls the CollectableManager to destroy collected items
        CollectableManager.CheckDestroyedItems();
    }
}
