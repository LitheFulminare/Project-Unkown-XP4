using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour, IInteractable
{
    public delegate bool Confirm();
    public static Confirm confirm;

    // video about enums
    // https://learn.unity.com/tutorial/enumerations#
    // https://www.youtube.com/watch?v=G4Qmy2sabpo

    public Items item;

    public Interaction player_interaction;

    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    public void Interact()
    {
        //Debug.Log(item); this would print pistolAmmo

        OverlayController.showUI();
        player_interaction.interact(item);
        Destroy(this.gameObject);
    }

    public bool checkIfPlayerConfirmed()
    {
        return true;
    }
}
