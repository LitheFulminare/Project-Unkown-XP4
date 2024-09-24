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

        for (int i = 0; i > collectables.Count; i++)
        {
            // gets whether the item got destroyed before and stores the data
            collectedItems[i] = collectables[i].isDestroyed;
        }
    }
}
