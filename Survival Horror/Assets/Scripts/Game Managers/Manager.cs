using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // keeps track of what the player is currently interacting with
    public static GameObject currentInteractionObj;

    [Header("Debug")]
    [SerializeField] private bool destroyEditorObjects = true;
    [SerializeField] private bool printDestroyedObjects = false;

    [SerializeField] private bool destroyLight = true;

    private void Start()
    {
        //List<GameObject> postProcessingObjects = new List<GameObject>();
        //postProcessingObjects.AddRange(GameObject.FindGameObjectsWithTag("Post Processing"));
        //foreach (var postProcessingObject in postProcessingObjects)
        //{

        //}

        if (destroyEditorObjects)
        {
            List<GameObject> editorObjects = new List<GameObject>();
            editorObjects.AddRange(GameObject.FindGameObjectsWithTag("EditorOnly"));

            foreach (GameObject obj in editorObjects)
            {
                Destroy(obj);

                // debug -> prints what objects were deactivated if the player marks a checkbox in editor
                if (printDestroyedObjects)
                {
                    Debug.Log($"Destroying the object: {obj.name}");
                }
            }
        }

        if (destroyLight)
        {
            List<GameObject> lights = new List<GameObject>();
            lights.AddRange(GameObject.FindGameObjectsWithTag("IngameLight"));

            foreach(GameObject light in lights)
            {
                Destroy(light);
            }
        }
    }
}
