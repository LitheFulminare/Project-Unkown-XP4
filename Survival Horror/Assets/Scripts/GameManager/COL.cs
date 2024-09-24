using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COL : MonoBehaviour
{

    [SerializeField] List<Collectable> collectables = new List<Collectable>();

    public static List<string> destroyedItems = new List<string>();

    public static List<bool> collectedItems = new List<bool>();

    private bool listGenerated = false;

    private void Start()
    {
        CheckRemainingItems();

        CheckDestroyedItems();
    }

    // prevents items from respawning
    private void CheckDestroyedItems()
    {
        // gets every item that was collected previously and destroys them when the scene reloads
        foreach (var name in destroyedItems)
        {
            GameObject item = GameObject.Find(name);
            item.SendMessage("selfDestruct"); // I violeted a naming convention here, but whatever
        }
    }
        

    private void CheckRemainingItems()
    {

        Debug.Log($"Items in the collectable list: {collectables.Count}");

        for (int i = 0; i < collectables.Count; i++)
        {
            // gets whether the item got destroyed before and stores the data
            if (!listGenerated)
            {
                //Debug.Log("list generated for the first time");
                collectedItems.Add(collectables[i].isDestroyed);
            }
            
            Debug.Log($"Item {i} collect status: {collectedItems[i]}");
        }

        listGenerated = true;
    }

    public static void AddDestroyedItem(string name)
    {
        destroyedItems.Add(name);
    }
}
