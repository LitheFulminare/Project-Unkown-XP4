using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistency : MonoBehaviour
{
    public delegate void AddPersistentElement(string gameObjectName);
    public static AddPersistentElement addItem;
    public static AddPersistentElement addPuzzle;

    public delegate void SavePersistencyList();
    public static SavePersistencyList savePersistencyList;

    public delegate void LoadPersistentObjects();
    public static LoadPersistentObjects loadPersistentObjects;

    private static RoomManager roomManager;

    // gets updated by 'WorldVars' on start
    public static List<string> destroyedItems = new List<string>();
    public static List<string> completedPuzzles = new List<string>();   

    private void OnEnable()
    {      
        addItem += AddDestroyedItem;
        addPuzzle += AddCompletedPuzzle;

        savePersistencyList += SaveList;
        savePersistencyList += BustRoomState;

        loadPersistentObjects += CheckDestroyedItems;
        loadPersistentObjects += CheckCompletePuzzles;
    }

    private void OnDisable()
    {
        addItem -= AddDestroyedItem;
        addPuzzle -= AddCompletedPuzzle;

        savePersistencyList -= SaveList;
        savePersistencyList -= BustRoomState;

        loadPersistentObjects -= CheckDestroyedItems;
        loadPersistentObjects -= CheckCompletePuzzles;
    }

    private void Start()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    private static void AddDestroyedItem(string itemName)
    {
        roomManager.PlayerAction();
        // check if item isnt already on the list, idk it might improve performance
        // COULD avoid unnecessary calls when loading the items, but I'm not sure
        destroyedItems.Add(itemName);
    }

    // called by 'SetCompleted' method on 'Interactable'
    private static void AddCompletedPuzzle(string puzzleName)
    {
        roomManager.PlayerAction();

        // because of how 'SetCompleted()' is structured, I have to check if the puzzle is not already on the list
        if (!completedPuzzles.Contains(puzzleName))
        {
            completedPuzzles.Add(puzzleName);
        }
    }

    // gets every item that was collected previously and destroys them when the scene loads
    private static void CheckDestroyedItems()
    {
        
        foreach (var itemName in destroyedItems)
        {
            GameObject itemObj = GameObject.Find(itemName);

            Collectable item = itemObj.GetComponent<Collectable>();
            if (item != null) item.SelfDestruct();
        }
    }

    // gets every puzzle that was completed previously and sets it as complete when the scene loads
    private static void CheckCompletePuzzles()
    {     
        foreach (var puzzleName in completedPuzzles)
        {
            GameObject puzzleObj = GameObject.Find(puzzleName);
            Interactable puzzle = puzzleObj.GetComponent<Interactable>();
            if (puzzle != null) puzzle.SetCompleted();

            //if (puzzle != null) { puzzle.SendMessage("SetCompleted"); }
        }
    }

    // temporary, this is not the best place
    private static void BustRoomState()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != "Bust Room")
        {
            //Debug.Log("Player not in Bust Room");
        }
        else
        {
            BustPuzzleManager manager = GameObject.FindAnyObjectByType<BustPuzzleManager>();

            if (manager != null)
            {
                manager.SaveAndLoadItems();
            }          
        }
    }

    // called by "SceneChanger" before loading the scene
    // sends what puzzles are completed on the current scene to WorldVars to make the data persistent
    private void SaveList()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        WorldVars.SaveCompletedPuzzles(completedPuzzles, sceneName);
        WorldVars.SaveDestroyedItems(destroyedItems, sceneName);
    }
}
