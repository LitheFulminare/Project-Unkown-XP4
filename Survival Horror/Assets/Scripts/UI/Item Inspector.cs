using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemInspector : MonoBehaviour
{
    public delegate void InspectItem(CollectableSO item);       
    public static InspectItem inspectItem;

    [SerializeField] GameObject spawnPosition;
    [SerializeField] private float rotationSpeed = 15f;

    private Transform currentItemInstance;

    private CollectableSO currentItem;

    // to prevent item from spawning twice
    private bool isInspecting = false;

    private GameObject itemGameObject;

    // used the clamp the X rotation when inpecting
    // this avoids weird gimbal lock flicks and locks the rotation smoothly
    readonly float minRotationX = -90f;
    readonly float maxRotationX = 90f;

    private void OnEnable()
    {
        inspectItem += SpawnItem;
    }

    private void OnDisable()
    {
        inspectItem -= SpawnItem;
    }

    private void Update()
    {
        currentItemInstance?.transform.Rotate(new Vector3 (0, 0, -1) * (rotationSpeed * Time.deltaTime));
    }

    // spawns/destroys the item to be inspected
    private void SpawnItem(CollectableSO item)
    {
        if (isInspecting) return;

        isInspecting = true;

        Debug.Log($"Inspecting {item.ingameName}");
        if (item == null)
        {
            Debug.Log("collectableSO on ItemInspector is null");
            return;
        }
        itemGameObject = Instantiate(item.inspectModel, spawnPosition.transform.position, Quaternion.identity);
        currentItemInstance = itemGameObject.transform;
    }

    // destroys the item instance
    private void StopInspecting()
    {
        Destroy(itemGameObject);
        currentItemInstance = null;
    }

    // called by 'InventoryController' using Unity's "mouse drag" interface
    //public void RotateItem(Vector3 mouseRotation)
    //{
    //    if (currentItemInstance != null)
    //    {
    //        // Calculate the new X rotation based on mouse input
    //        Vector3 currentEulerAngles = currentItemInstance.localEulerAngles;
    //        float newRotationX = currentEulerAngles.x + mouseRotation.x;

    //        // Ensure that the X rotation wraps correctly in the range [0, 360] degrees
    //        newRotationX = (newRotationX + 360) % 360;

    //        // Clamp the X rotation within the limits (-90 to 90 degrees)
    //        if (newRotationX > 180f) newRotationX -= 360f;  // Convert to [-180, 180] range
    //        newRotationX = Mathf.Clamp(newRotationX, minRotationX, maxRotationX);

    //        // Apply the X rotation only to the X axis, and keep Y and Z as they are
    //        currentItemInstance.localEulerAngles = new Vector3(newRotationX, currentEulerAngles.y + mouseRotation.y);      
            
    //        // gpt made this method btw, I couldn't solve he gimbal lock flicks by myself
    //    }
    //}
}
