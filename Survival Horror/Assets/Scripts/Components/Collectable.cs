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

    public Items item;

    // determines if the item should be sent to the regular inventory or to the document section
    // probaly will be stored by the CollectableSO instead
    [SerializeField] public bool isDocument = false;

    private void Start()
    {
        confirm = checkIfPlayerConfirmed;
    }

    public void Interact()
    {
        OverlayController.showUI(gameObject, collectableSO);
    }

    // called when collected
    // also called by 'CheckDestroyedItems' method on the 'DataPersistency' class to prevent item from respawning
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
        SelfDestruct();
        DataPersistency.addItem(gameObject.name); // marks this item as collected, so it won't respawn when the scene reloads      
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
}
