using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    // follow this tutorial to make the interactor system and how interfaces work (Unity 2021)
    // https://www.youtube.com/watch?v=K06lVKiY-sY
    // https://learn.unity.com/tutorial/interfaces#6352660eedbc2a5f87a1aa6c

    public Transform InteractorSource;
    public float InteractRange = 4.0f;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("F was pressed");
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
