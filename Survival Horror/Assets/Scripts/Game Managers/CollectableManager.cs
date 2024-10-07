using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour // this will mainly keep track of destroyed items and destroy those on start
{
    [SerializeField] RoomManager roomManager;

    // keeps track of destroyed items
    // gets updated by 'WorldVars' on start
    public static List<string> destroyedItems = new List<string>();

    // Called by 'WorldVars' after loading the destroyed items list
    // prevents items from respawning
    // destroys the items in the list
    public static void CheckDestroyedItems()
    {
        // gets every item that was collected previously and destroys them when the scene reloads
        foreach (var itemName in destroyedItems)
        {
            GameObject item = GameObject.Find(itemName);
            if (item != null) { item.SendMessage("selfDestruct"); } // I violeted a naming convention here, but whatever
        }
    }

    // called by 'Collectable' in 'Collected()', receives the gameObject name
    public static void AddDestroyedItem(string itemName)
    {
        destroyedItems.Add(itemName);
    }

    // called by "SceneChanger" before loading the scene
    // sends what items were destroyed to WorldVars to make the data persistent
    public void SaveList()
    {
        WorldVars.SaveDestroyedItems(destroyedItems, SceneManager.GetActiveScene().name);
    }
}
