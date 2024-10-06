using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInspector : MonoBehaviour
{
    public delegate void InspectItem(Items item);       
    public static InspectItem inspectItem;

    [SerializeField] GameObject spawnPosition;

    [SerializeField] GameObject pistol;

    private GameObject currentItem;

    private void OnEnable()
    {
        inspectItem += SpawnItem;
    }

    private void OnDisable()
    {
        inspectItem -= SpawnItem;
    }

    private void SpawnItem(Items item)
    {
        Debug.Log("SpawnItem was called");
        switch(item)
        {
            case (Items.pistol): currentItem = pistol; break;

            default: Debug.Log("ItemInspector tried to spawn an unassigned item"); break;
        }

        if (currentItem != null)
        {
            GameObject currentItemInstance = Instantiate(currentItem);
            currentItemInstance.transform.position = spawnPosition.transform.position;
        }
    }

    private void StopInspection()
    {
        if (currentItem != null) { Destroy(currentItem); }
        else { Debug.LogError("ItemInspector tried to destroy a null object"); }
    }
}
