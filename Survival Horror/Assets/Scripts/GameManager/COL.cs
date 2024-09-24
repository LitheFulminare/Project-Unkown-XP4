using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COL : MonoBehaviour
{
    // this doesnt have any used right now, but who knows?
    [SerializeField] List<Collectable> collectables = new List<Collectable>();

    // keeps track of destroyed items
    public static List<string> destroyedItems = new List<string>();

    private void Start()
    {
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

        Debug.Log($"Items destroyed: {destroyedItems.Count}");
    }

    // called by 'Collectable' in 'Collected()', receives the gameObject name
    public static void AddDestroyedItem(string name)
    {
        destroyedItems.Add(name);
    }
}
