using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVars : MonoBehaviour
{
    // these only get updated when SceneChanger calls InventoryController
    public static Items[] itemList;
    public static int[] itemCount;

    [SerializeField] InventoryController inventoryController;

    private void Start()
    {
        if (itemList == null && itemCount == null)
        {
            Debug.Log("The inventory is empty");
        }
        else
        {
            Debug.Log($"First item on the inventory: {itemList[0]}");

            inventoryController.LoadInventory(itemList, itemCount);
        }
    }
}
