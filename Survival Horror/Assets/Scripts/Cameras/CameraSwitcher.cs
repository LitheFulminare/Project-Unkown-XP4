using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    private static int order = 0;

    public Transform Player;
    public CinemachineVirtualCamera activeCam;
    public Canvas canvas;

    [SerializeField] bool printDebugErrors = false;

    private void Start()
    {
        order = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // change camera
            if (activeCam != null)
            {
                activeCam.Priority = 1;
                
            }
            else if (printDebugErrors)
            {
                Debug.LogError("CameraSwicher could not find the Cinemachine VC.");             
            }

            // change background image
            if (canvas! != null)
            {
                canvas.enabled = true;
                order++;
                canvas.sortingOrder = order;
            }
            else if (printDebugErrors)
            {
                Debug.LogError("CameraSwitcher could not find the canvas");
            }

            // synchronizes the main camera and thermal cameras' field of view
            Manager.syncCameras?.Invoke(activeCam.m_Lens.FieldOfView); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // change camera
            if (activeCam != null)
            {
                activeCam.Priority = 0;
            }
            else if (printDebugErrors)
            {
                Debug.LogError("CameraSwicher could not find the Cinemachine VC.");
            }

            // change background image
            if (canvas! != null)
            {
                canvas.enabled = false;
            }
            else if (printDebugErrors)
            {
                Debug.LogError("CameraSwitcher could not find the canvas");
            }
        }
    }
}
