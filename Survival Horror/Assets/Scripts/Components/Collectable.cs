using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collectable : MonoBehaviour, IInteractable
{
    public delegate void ConfirmAction(bool playerConfirmed, GameObject itemChecker);
    public static ConfirmAction confirm;

    [SerializeField] public CollectableSO collectableSO;

    public bool isDestroyed = false;

    // video about enums
    // https://learn.unity.com/tutorial/enumerations#
    // https://www.youtube.com/watch?v=G4Qmy2sabpo

    public Items item;

    // determines if the item should be sent to the regular inventory or to the document section
    [SerializeField] public bool isDocument = false;

    //public Interaction player_interaction;


    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    public void Interact()
    {
        OverlayController.showUI(gameObject, collectableSO);
    }

    // this probably wont be used
    public void checkIfPlayerConfirmed(bool confirm, GameObject itemChecker)
    {
        //Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
        if (confirm)// && itemChecker == gameObject)
        {
            //player_interaction.interact(item);
             
        }

        //Debug.Log("checkIfPlayerConfirmed parameter: " + confirm);
    }

    public void SelfDestruct()
    {
        isDestroyed = true;
        gameObject.SetActive(false);
    }

    // called by ButtonAction in the Overlay Controller if the player accepts to pick up the item
    private void collected()
    {
        //isDestroyed = true;

        // find an empty space and adds item to the stack, then it self destructs
        InventoryController.itemReceiver(collectableSO);
        CollectableManager.AddDestroyedItem(gameObject.name); // marks this item as collected, so it won't respawn when the scene reloads
        SelfDestruct();
    }
}
