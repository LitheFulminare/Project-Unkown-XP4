using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform Player;
    public CinemachineVirtualCamera activeCam;
    public Canvas canvas;

    [SerializeField] bool printDebugErrors = false;

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
            }
            else if (printDebugErrors)
            {
                Debug.LogError("CameraSwitcher could not find the canvas");
            }          
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
