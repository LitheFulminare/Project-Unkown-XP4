using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVars : MonoBehaviour
{
    // these only get updated when 'SceneChanger' calls InventoryController
    public static Items[] itemList;
    public static int[] itemCount;

    // get by player to know if it's allowed to move or not
    // can only be set by the public static funcion "BlocMovement"
    public static bool isMovementBlocked { get; private set; } = false;

    // set by 'SceneChanger'
    public static Vector3 spawnPosition;

    [SerializeField] InventoryController inventoryController;
    //[SerializeField] COL col;

    // create a index for each scene and use it to create this and access this
    public static List<List<GameObject>> destroyedItemsPerRoom = new List<List<GameObject>>();

    // stores what items were destroyed
    // there is a list for each room
    public static List<string> destroyedItemsRoom1 = new List<string>();
    public static List<string> destroyedItemsRoom2 = new List<string>();
    public static List<string> destroyedItemsRoom3 = new List<string>();

    // create Enums for room status: unexplored, partially explored and completely explored
    // this will be used to display stuff on the map

    private void Start()
    {
        //GetSceneDestroyedItems();

        // loads the inventory data if there is any
        // this is mainly used when switch scenes

        Debug.Log($"destroyedItemsRoom1.Count: {destroyedItemsRoom1.Count}");

        if (itemList == null && itemCount == null)
        {
            //Debug.Log("The inventory is empty");
        }
        else
        {
            inventoryController.LoadInventory(itemList, itemCount);
        }

        // loads the destroyed items on the collectable manager
        CollectableManager.destroyedItems.Clear();
        switch (SceneManager.GetActiveScene().name)
        {       
            case "Scene1": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom1); break;
            case "Scene2": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom2); break;
            case "Scene3": CollectableManager.destroyedItems.AddRange(destroyedItemsRoom3); break;
        }      
    }

    // used by UI elements and some other things to block player movement
    public static void BlockMovement(bool par)
    {
        isMovementBlocked = par;
    }

    // Called by 'SceneChanger' after loading the new scene
    public static void UpdateSpawnPosition(Vector3 newPos)
    {
        spawnPosition = newPos;
    }

    public static void SaveDestroyedItems(List<string> destroyedItems, string sceneName)
    {
        switch (sceneName) // destroyedItemsRoom
        {
            case "Scene1": destroyedItemsRoom1.AddRange(destroyedItems); break;
            case "Scene2": destroyedItemsRoom2.AddRange(destroyedItems); break;
            case "Scene3": destroyedItemsRoom3.AddRange(destroyedItems); break;
        }
    }
}
