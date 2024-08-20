using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success )
        {
            // calculate the direction
            var direction = position - transform.position;

            // ignore the height difference
            direction.y = 0;

            // make the transform look in the direction
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // if the raycast hits something, the position will be returned
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // in case the raycast doesn't hit anything
            return (success: false, position: Vector3.zero);
        }
    }
}
