using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour // this will mainly keep track of destroyed items and destroy those on start
{
    private static RoomManager roomManager;

    // keeps track of destroyed items
    // gets updated by 'WorldVars' on start
    public static List<string> destroyedItems = new List<string>();

    private void Start()
    { 
        // cannot be serialized since its store in a static field
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }

    // Called by 'WorldVars' after loading the destroyed items list
    // prevents items from respawning
    // destroys the items in the list
    public static void CheckDestroyedItems()
    {
        // gets every item that was collected previously and destroys them when the scene reloads
        foreach (var itemName in destroyedItems)
        {
            GameObject itemObj = GameObject.Find(itemName);

            Collectable item = itemObj.GetComponent<Collectable>();
            if (item != null) item.SelfDestruct();
        }
    }

    // called by 'Collected' method in 'Collectable' class
    public static void AddDestroyedItem(string itemName)
    {
        roomManager.PlayerAction();
        destroyedItems.Add(itemName);
    }

    // called by 'SceneChanger' before loading the scene
    // sends what items were destroyed to WorldVars to make the data persistent
    public void SaveList()
    {
        WorldVars.SaveDestroyedItems(destroyedItems, SceneManager.GetActiveScene().name);
    }
}
