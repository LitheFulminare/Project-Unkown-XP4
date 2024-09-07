using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
    public static ConfirmAction confirm;

    //public Items item;

    [SerializeField] Items neededItem;

    public Interaction player_interaction;

    public void Interact()
    {
        Debug.Log("Player interacted with door");
        //OverlayController.showUI(gameObject, item); create other function to show custom text
        //OverlayController.interactOverlay(gameObject, neededItem);
    }
}

