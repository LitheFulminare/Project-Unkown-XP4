using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInspector : MonoBehaviour
{
    public delegate void InspectItem(Items item);       
    public static InspectItem inspectItem;

    [SerializeField] GameObject spawnPosition;

    [SerializeField] GameObject pistol;

    private Transform itemTransform;

    private GameObject currentItem;
    GameObject currentItemInstance;
    // used to know if should spawn or destroy the item
    // probably is temporary, destroying will have a dedicated button
    // then it would be easier to apply a 'fade' or 'darken' effect on the inventory screen
    public static bool isInspecting = false; 

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

            // sees what item should spawn
            switch (item)
            {
                case (Items.pistol): currentItem = pistol; break;

                default: Debug.Log("ItemInspector tried to spawn an unassigned item"); break;
            }

            if (currentItem != null)
            {
                // spawns the item
                //currentItemInstance = Instantiate(currentItem);
                //currentItemInstance.transform.position = spawnPosition.transform.position;

                GameObject itemGameObject = Instantiate(currentItem, spawnPosition.transform.position, Quaternion.identity);
                itemTransform = itemGameObject.transform;
                //itemTransform.position = spawnPosition.transform.position;
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

    public void RotateItem(Vector3 mouseRotation)
    {
        if (itemTransform != null)
        {
            //currentItemInstance.transform.eulerAngles += mouseRotation;

            if (itemTransform.localRotation.x < 90 && itemTransform.eulerAngles.x + mouseRotation.x < 90)
            {
                itemTransform.eulerAngles += mouseRotation;
            }
            else if (itemTransform.localRotation.x > 270 && itemTransform.eulerAngles.x + mouseRotation.x > 270)
            {
                itemTransform.eulerAngles += mouseRotation;
            }
            else
            {
                Debug.Log($"x angle: {itemTransform.eulerAngles.x}");
            }

            //Quaternion quatRotation = Quaternion.Euler(mouseRotation);
            //itemTransform.localRotation *= quatRotation;           
        }
    }
}
