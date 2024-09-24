using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COL : MonoBehaviour
{

    [SerializeField] List<Collectable> collectables = new List<Collectable>();

    public static bool[] collectedItems;

    private void Start()
    {
        CheckRemainingItems();
    }

    private void CheckRemainingItems()
    {
        collectedItems = new bool[collectables.Count];

        Debug.Log($"Items in the collectable list: {collectables.Count}");
        for (int i = 0; i < collectables.Count; i++)
        {
            // gets whether the item got destroyed before and stores the data
            collectedItems[i] = collectables[i].isDestroyed;
            Debug.Log($"Item {i} collect status: {collectables[i].isDestroyed}");
        }
    }
}
