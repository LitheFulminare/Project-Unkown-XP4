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
    public delegate void PickupOverlay(GameObject collectedItem, CollectableSO itemName);
    public static PickupOverlay showUI; // this is used to show the overlay when an item is picked

    public delegate void InteractOverlay(GameObject interactedItem, CollectableSO neededItem, string description, string prompt, bool r, CollectableSO cSO);
    public static InteractOverlay interactOverlay; // // this is used to show the overlay when the player interacts and a custom text is needed

    public delegate void Overlay();
    public static Overlay overlayOpened;
    public static Overlay overlayClosed;

    public GameObject canvas;

    // used to call their functions to be collected / interacted with
    public GameObject collectedItem;

    public GameObject interactedItem;
    public CollectableSO itemNeeded;

    [SerializeField] Text topText;
    [SerializeField] Text redText;
    [SerializeField] Text bottomText;

    private string collectedItemName;

    // used when the player places an item and retrieves it
    private bool isRetrieving;
    private CollectableSO currentItem;

    private bool _isCollectable = false;

    private void Start()
    {
        canvas.SetActive(false);

        showUI = setActive;
        interactOverlay = setActiveInteract;
    }

    // called by 'Interact' method on 'Collectable', shows the pickup overlay
    public void setActive(GameObject collectedItem, CollectableSO collectableSO)
    {
        // i dont think it will be used, i didnt really like it
        //AudioManager.instance.PlayOneShot(FMODEvents.instance.open, this.transform.position);

        canvas.SetActive(true);
        this.collectedItem = collectedItem;
        ItemInspector.inspectItem(collectableSO);
        PlayerVars.BlockPlayer(true);

        topText.text = "You found ";
        bottomText.text = "Take it?";

        redText.text = collectableSO.ingameName;

        //switch (itemName)
        //{
        //    // regular collectable items
        //    case Items.pistolAmmo: redText.text = "pistol ammo"; break;
        //    case Items.pistol: redText.text = "pistol"; break;
        //    case Items.syringe: redText.text = "syringe"; break;
        //    case Items.keyDoor1: redText.text = "door key"; break;

        //    // puzzle items
        //    case Items.bustAGoat: redText.text = "goat bust statue"; break;
        //    case Items.bustBBear: redText.text = "bear bust statue"; break;
        //    case Items.bustCMonkey: redText.text = "monkey bust statue"; break;
        //    case Items.bustDBull: redText.text = "bull bust statue"; break;
        //    case Items.bustEHorse: redText.text = "horse bust statue"; break;
        //}

        OverlayController.overlayOpened?.Invoke();
        _isCollectable = true;
        currentItem = null;
        isRetrieving = false;
    }

    // this is too messy, I think I should break this down
    public void setActiveInteract(GameObject interactedItem, CollectableSO itemNeeded, string description, string prompt, bool isRetrieving, CollectableSO currentItem)
    {
        OverlayController.overlayOpened?.Invoke();

        // prob wont be used
        //AudioManager.instance.PlayOneShot(FMODEvents.instance.open, this.transform.position);

        canvas.SetActive(true);
        this.interactedItem = interactedItem; 
        this.itemNeeded = itemNeeded;

        PlayerVars.BlockPlayer(true);

        redText.text = "";

        topText.text = description;
        bottomText.text = prompt;

        _isCollectable = false;

        if (currentItem != null) this.currentItem = currentItem;
        this.isRetrieving = isRetrieving;
    }

    // called by the 'confirm' and 'deny' buttons the pickup screen overlay
    // 'confirm' returns true and 'deny' returns false
    // also either of the buttons blocks player movements
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
                if (!isRetrieving)
                {
                    // tells the Inventory Controller what the player needs and opens the screen
                    InventoryController.setItemNeeded(itemNeeded);
                    PlayerVars.BlockPlayer(false); // this need to be here cuz if the player is blocked, the inventory wont open
                    InventoryController.callInventory();
                }
                else
                {
                    InventoryController.itemReceiver(currentItem);
                    interactedItem.SendMessage("ItemRetrieved");
                }              
            }
        }

        PlayerVars.BlockPlayer(false);
        OverlayController.overlayClosed?.Invoke();
        ItemInspector.destroyItem?.Invoke();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonSelected, this.transform.position);
    }

    // called by an 'EventTrigger' on the button object
    // used to play a sound
    public void ButtonHover()
    {
       // There used to be a hover sound, but it was too goofy
    }
}
