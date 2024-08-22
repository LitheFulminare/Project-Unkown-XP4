using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public PlayerControls playerControls;

    private InputAction interact; // F key

    void Start()
    {

    }

    void Update()
    {

    }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        //playerControls.Enable();

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        //playerControls.Disable();
        interact.Disable();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Coisa");
    }
}