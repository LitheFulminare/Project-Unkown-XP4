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
    GameObject currentItemInstance;
    private bool isInspecting = false; // used to know if should spawn or destroy the item, probably is temporary, destroying will have a dedicated button

    private void OnEnable()
    {
        inspectItem += SpawnItem;
    }

    private void OnDisable()
    {
        inspectItem -= SpawnItem;
    }

    // spawns/destroys the item to be inspected
    private void SpawnItem(Items item)
    {
        if (!isInspecting)
        {
            isInspecting = true;
            Debug.Log("SpawnItem was called");
            switch (item)
            {
                case (Items.pistol): currentItem = pistol; break;

                default: Debug.Log("ItemInspector tried to spawn an unassigned item"); break;
            }

            if (currentItem != null)
            {
                currentItemInstance = Instantiate(currentItem);
                currentItemInstance.transform.position = spawnPosition.transform.position;
            }
        }
        else
        {
            StopInspecting();
        }
    }

    // destroys the item instance
    private void StopInspecting()
    {
        if (currentItem != null)
        {
            Destroy(currentItemInstance); 
            currentItem = null;
        }
        else 
        { 
            Debug.LogError("ItemInspector tried to destroy a null object"); 
        }

        isInspecting = false;
    }
}
