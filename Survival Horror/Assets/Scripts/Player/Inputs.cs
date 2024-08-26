using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    // inputs
    public PlayerControls playerControls;
    private InputAction interact; // F key

    // events
    // https://learn.unity.com/tutorial/events-uh#
    public delegate void interact_event();

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