using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldVars : MonoBehaviour // serves a similar purpose to PlayerVars, it stores data like items collected and puzzles done
{
    // Dictionaries to hold lists of destroyed items and completed puzzles for each scene
    private static Dictionary<string, List<string>> destroyedItemsPerRoom = new Dictionary<string, List<string>>();
    private static Dictionary<string, List<string>> completedPuzzlesPerRoom = new Dictionary<string, List<string>>();

    // Static bool to ensure the lists are only generated once
    private static bool isInitialized = false;

    [SerializeField] RoomManager roomManager;

    void Awake()
    {
        // Only initialize the room data at the start of the game
        if (!isInitialized)
        {
            InitializeRoomData();
        }
    }

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        LoadPersistentObjects(sceneName);

        roomManager.InitializeRoomState();
    }

    // called on awake, creates a list for each Scene addded to the build and adds to the dictionary
    private void InitializeRoomData()
    {
        // Loop through all the scenes added in the Build Settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // Create empty lists for destroyed items and completed puzzles for this scene
            if (!destroyedItemsPerRoom.ContainsKey(sceneName))
            {
                destroyedItemsPerRoom.Add(sceneName, new List<string>());
                completedPuzzlesPerRoom.Add(sceneName, new List<string>());
            }
        }

        isInitialized = true;
    }

    // called by 'DataPersistency' after SceneChanger calls it to save the items
    public static void SaveDestroyedItems(List<string> destroyedItems, string sceneName)
    {
        if (destroyedItemsPerRoom.ContainsKey(sceneName))
        {
            destroyedItemsPerRoom[sceneName].Clear();
            destroyedItemsPerRoom[sceneName].AddRange(destroyedItems);
        }
    }

    // same thing as above but for puzzles
    public static void SaveCompletedPuzzles(List<string> completedPuzzles, string sceneName)
    {
        if (completedPuzzlesPerRoom.ContainsKey(sceneName))
        {
            completedPuzzlesPerRoom[sceneName].Clear();
            completedPuzzlesPerRoom[sceneName].AddRange(completedPuzzles);
        }
    }

    private void LoadPersistentObjects(string sceneName)
    {
        // I could create a function on DataPersistency to clear and add the list, so the code is cleaner here

        DataPersistency.destroyedItems.Clear();
        DataPersistency.destroyedItems.AddRange(destroyedItemsPerRoom[sceneName]);

        DataPersistency.completedPuzzles.Clear();
        DataPersistency.completedPuzzles.AddRange(completedPuzzlesPerRoom[sceneName]);

        DataPersistency.loadPersistentObjects();
    }


    // these two prob wont be used. 'LoadPersistentObjects' basically does the same thing
    private void LoadDestroyedItems(string sceneName)
    {
        DataPersistency.destroyedItems.Clear();
        DataPersistency.destroyedItems.AddRange(destroyedItemsPerRoom[sceneName]);

        // destroy collected items and update solved puzzles
        DataPersistency.loadPersistentObjects();
    }   private void LoadCompletedPuzzles(string sceneName)
    {
        DataPersistency.completedPuzzles.Clear();
        DataPersistency.completedPuzzles.AddRange(completedPuzzlesPerRoom[sceneName]);

        // destroy collected items and update solved puzzles
        DataPersistency.loadPersistentObjects();
    }
}
