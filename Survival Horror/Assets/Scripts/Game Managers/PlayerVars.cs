using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVars : MonoBehaviour
{
    public delegate void OnPlayerRestrained();
    public static OnPlayerRestrained onPlayerRestrained;

    public delegate void OnPlayerFreed();
    public static OnPlayerFreed onPlayerFreed;

    // these only get updated when 'SceneChanger' calls InventoryController
    public static CollectableSO[] itemList;
    public static int[] itemCount;
    public static DocumentSO[] documentList;

    // get by player to know if it's allowed to move or not
    // can only be set by the public static funcion "BlockPlayer"
    public static bool playerBlocked { get; private set; } = false;

    // set by 'SceneChanger'
    public static Vector3 spawnPosition;

    [SerializeField] InventoryController inventoryController;
    //[SerializeField] COL col;

    // create Enums for room status: unexplored, partially explored and completely explored
    // this will be used to display stuff on the map

    private void Start()
    {
        // loads the inventory data if there is any
        // this is mainly used when switch scenes

        if (itemList == null && itemCount == null)
        {
            //Debug.Log("The inventory is empty");
        }
        else
        {
            inventoryController.LoadInventory(itemList, itemCount);
        }
    }

    // used by UI elements and some other things to block player movement
    public static void BlockPlayer(bool value)
    {
        // blocks the player
        if (value)
        {
            onPlayerRestrained.Invoke();
        }

        // unblocks the player
        else
        {
            onPlayerFreed.Invoke();
        }

        playerBlocked = value;       
    }

    // Called by 'SceneChanger' after loading the new scene
    public static void UpdateSpawnPosition(Vector3 newPosition)
    {
        spawnPosition = newPosition;
    }
}
