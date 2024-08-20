using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // used the following tutorial:
    // https://www.youtube.com/watch?v=149teLQMmOQ

    [SerializeField] private Camera mainCamera;
    
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            // raycastHit.point returns the position where the raycast hit
            transform.position = raycastHit.point;
        }
        
    }
}
