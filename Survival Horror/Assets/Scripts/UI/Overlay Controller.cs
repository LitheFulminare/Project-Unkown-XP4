using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

// interfaces
// https://learn.unity.com/tutorial/interfaces#

// events
// https://learn.unity.com/tutorial/events-uh#
// https://gamedevbeginner.com/events-and-delegates-in-unity/
public class OverlayController : MonoBehaviour
{
    // used to precache font
    private static readonly string kPrecacheFontGlyphsString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_+=~`[]{}|\\:;\"'<>,.?/ ";

    public delegate void PickupOverlay(GameObject collectedItem, Items itemName);
    public static PickupOverlay showUI; // this is used to show the overlay when an item is picked

    public delegate void InteractOverlay(GameObject interactedItem, Items neededItem);
    public static InteractOverlay interactOverlay; // // this is used to show the overlay when the player interacts and a custom text is needed

    public GameObject canvas;

    // used to call their functions to be collected / interacted with
    public GameObject collectedItem;

    public GameObject interactedItem;
    public Items itemNeeded;

    [SerializeField] Text topText;
    [SerializeField] Text redText;
    [SerializeField] Text bottomText;

    private string collectedItemName;
    

    private bool _isCollectable = false;

    private void Awake()
    {
        //PreCacheFontData();
    }

    private void Start()
    {
        canvas.SetActive(false);

        //using UnityEngine.EventSystems
        //EventSystemManager.currentSystem.SetSelectedGameObject(defaultButton, null);

        showUI = setActive;
        interactOverlay = setActiveInteract;
    }

    void PreCacheFontData()
    {
        redText.text = kPrecacheFontGlyphsString;
    }

    // called by 'collectable', shows the pickup overlay
    public void setActive(GameObject collectedItem, Items itemName)
    {
        //collectedItemName = collectedItem.name;
        //Debug.Log(itemName);

        canvas.SetActive(true);
        this.collectedItem = collectedItem;

        topText.text = "You found ";
        bottomText.text = "Take it?";

        switch (itemName)
        {
            case Items.pistolAmmo:
                redText.text = "pistol ammo"; break;
            case Items.syringe:
                redText.text = "syringe"; break;
            case Items.keyDoor1:
                redText.text = "door key"; break;
        }

        _isCollectable = true;
    }

    public void setActiveInteract(GameObject interactedItem, Items itemNeeded)
    {
        canvas.SetActive(true);
        this.interactedItem = interactedItem; 
        this.itemNeeded = itemNeeded;

        if (itemNeeded == Items.keyDoor1)
        {
            topText.text = "There is a "; redText.text = "door";

            bottomText.text = "Use item?";
        }     

        _isCollectable = false;
    }

    // called by the 'confirm' and 'deny' buttons the pickup screen overlay
    // 'confirm' returns true and 'deny' returns false
    public void ButtonAction(bool playerAction)
    {
        if (_isCollectable)
        {
            if (playerAction)
            {        
                // search object by name and then call the function to be collected
                GameObject itemToDestroy = GameObject.Find(collectedItem.name);
                itemToDestroy.SendMessage("collected");
            }
        }

        else if (!_isCollectable)
        {
            if (playerAction)
            {
                // tells the Inventory Controller what the player needs and opens the screen
                InventoryController.setItemNeeded(itemNeeded);
                InventoryController.callInventory();
            }
        }
        
    }


}
