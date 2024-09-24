using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour
{
    // these only get updated when SceneChanger calls InventoryController
    public static Items[] itemList;
    public static int[] itemCount;

    // get by player to know if it's allowed to move or not
    // can only be set by the public static funcion "BlocMovement"
    public static bool isMovementBlocked { get; private set; } = false;

    public static Vector3 spawnPosition;

    [SerializeField] InventoryController inventoryController;

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
            //Debug.Log($"First item on the inventory: {itemList[0]}");

            inventoryController.LoadInventory(itemList, itemCount);
        }
    }

    // used UI elements to block player movement
    public static void BlockMovement(bool par)
    {
        isMovementBlocked = par;
    }
}
