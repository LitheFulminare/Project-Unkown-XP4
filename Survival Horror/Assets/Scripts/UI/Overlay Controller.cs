using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// interfaces
// https://learn.unity.com/tutorial/interfaces#

// events
// https://learn.unity.com/tutorial/events-uh#
// https://gamedevbeginner.com/events-and-delegates-in-unity/



public class OverlayController : MonoBehaviour
{
    public delegate void PickupOverlay(GameObject collectedItem, Items itemName);
    public static PickupOverlay showUI; // this is used to show the overlay when an item is picked

    public GameObject canvas;

    public GameObject collectedItem;

    [SerializeField] Text text;

    private string collectedItemName;

    private void Start()
    {
        canvas.SetActive(false);

        //using UnityEngine.EventSystems
        //EventSystemManager.currentSystem.SetSelectedGameObject(defaultButton, null);

        showUI = setActive;
    }

    // called by 'collectable', shows the pickup overlay
    public void setActive(GameObject collectedItem, Items itemName)
    {
        //collectedItemName = collectedItem.name;
        //Debug.Log(itemName);

        canvas.SetActive(true);
        this.collectedItem = collectedItem;

        switch (itemName)
        {
            case Items.pistolAmmo:
                text.text = "pistol ammo"; break;
            case Items.syringe:
                text.text = "syringe"; break;
            case Items.keyDoor1:
                text.text = "door key"; break;
        }

        
    }

    // called by the 'confirm' and 'deny' buttons the pickup screen overlay
    // 'confirm' returns true and 'deny' returns false
    public void ButtonAction(bool playerAction)
    {

        if (playerAction)
        {        
            // search object by name and then call the function to be collected
            GameObject itemToDestroy = GameObject.Find(collectedItem.name);
            itemToDestroy.SendMessage("collected");
        }
    }
}
