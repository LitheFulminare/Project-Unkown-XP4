using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // keeps track of what the player is currently interacting with
    public static GameObject currentInteractionObj;

    // for debug purposes
    [SerializeField] private bool printDeactivatedObjects = false;

    private void Start()
    {
        List<GameObject> editorObjects = new List<GameObject>();
        editorObjects.AddRange(GameObject.FindGameObjectsWithTag("EditorOnly"));

        foreach (GameObject obj in editorObjects)
        {            
            obj.SetActive(false);

            // debug -> prints what objects were deactivated if the player marks a checkbox in editor
            if (printDeactivatedObjects)
            {
                Debug.Log($"Deactivating the object: {obj.name}");
            }
        }        
    }
}
