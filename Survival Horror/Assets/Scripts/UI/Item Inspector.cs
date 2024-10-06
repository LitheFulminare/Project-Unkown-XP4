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

    private GameObject currentItem;
    private Transform currentItemInstance;

    private GameObject itemGameObject;

    // used to know if should spawn or destroy the item
    // probably is temporary, destroying will have a dedicated button
    // then it would be easier to apply a 'fade' or 'darken' effect on the inventory screen
    public static bool isInspecting = false;

    // used the clamp the X rotation when inpecting
    // this avoids weird gimbal lock flicks and locks the rotation smoothly
    float minRotationX = -90f;
    float maxRotationX = 90f;

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
                itemGameObject = Instantiate(currentItem, spawnPosition.transform.position, Quaternion.identity);
                currentItemInstance = itemGameObject.transform;
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
            Destroy(itemGameObject);
            currentItemInstance = null;
        }
        else 
        { 
            Debug.LogError("ItemInspector tried to destroy a null object"); 
        }

        isInspecting = false;
    }

    // called by 'InventoryController' using Unity's "mouse drag" interface
    public void RotateItem(Vector3 mouseRotation)
    {
        if (currentItemInstance != null)
        {
            // Calculate the new X rotation based on mouse input
            Vector3 currentEulerAngles = currentItemInstance.localEulerAngles;
            float newRotationX = currentEulerAngles.x + mouseRotation.x;

            // Ensure that the X rotation wraps correctly in the range [0, 360] degrees
            newRotationX = (newRotationX + 360) % 360;

            // Clamp the X rotation within the limits (-90 to 90 degrees)
            if (newRotationX > 180f) newRotationX -= 360f;  // Convert to [-180, 180] range
            newRotationX = Mathf.Clamp(newRotationX, minRotationX, maxRotationX);

            // Apply the X rotation only to the X axis, and keep Y and Z as they are
            currentItemInstance.localEulerAngles = new Vector3(newRotationX, currentEulerAngles.y + mouseRotation.y);      
            
            // gpt made this method btw, I couldn't solve he gimbal lock flicks by myself
        }
    }
}
