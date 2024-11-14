using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public delegate void SyncCameras(float fieldOfView);
    public static SyncCameras syncCameras;

    // keeps track of what the player is currently interacting with
    public static GameObject currentInteractionObj;

    [Header("Debug")]
    [SerializeField] private bool destroyEditorObjects = true;
    [SerializeField] private bool printDestroyedObjects = false;

    [SerializeField] private bool destroyLight = true;

    private Camera thermalCamera;

    private void OnEnable()
    {
        syncCameras += SynchronizeCameras;
    }

    private void OnDisable()
    {
        syncCameras -= SynchronizeCameras;
    }

    private void Start()
    {
        GetThermalCamera();

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

    private void GetThermalCamera()
    {
        GameObject thermalCameraObj = GameObject.FindGameObjectWithTag("Thermal Camera");

        if (thermalCameraObj == null)
        {
            Debug.LogWarning("Could not find object with 'Thermal Camera' tag");
            return;
        }

        if (!thermalCameraObj.TryGetComponent<Camera>(out thermalCamera))
        {
            Debug.LogWarning("Failed to find the Thermal Camera");
        }
    }

    private void SynchronizeCameras(float fieldOfView)
    {
        if (thermalCamera == null)
        {
            Debug.LogWarning("Failed to sync cameras' FOV: thermalCamera was null");
            return;
        }

        thermalCamera.fieldOfView = fieldOfView;
    }
}
