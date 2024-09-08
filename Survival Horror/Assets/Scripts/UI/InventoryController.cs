using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static InventoryController;
//using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public delegate void ItemReceiver(Items item);
    public static ItemReceiver itemReceiver; // used to add item to inventory
    public static ItemReceiver useItem; // used to complete a puzzle/interaction
    public static ItemReceiver setItemNeeded; // self explanatory

    public delegate void ToggleInventory();
    public static ToggleInventory callInventory;

    public Items[] itemList = new Items[6]; // list of the items
    public int[] itemCount = new int[6]; // list of how many items the player has

    public GameObject canvas;

    public ItemSpawner itemSpawner; // what calls each icon to show items in inventory

    private Items _itemNeeded; // set when the inventory is show because of a puzzle/interaction

    [SerializeField] private GameObject textPopup;

    private void Start()
    {
        itemReceiver = addToInventory; // called by the collectable
        useItem = UseItem; // called by 'ShowIcon' when the 'Button' component on the icon is pressed
        callInventory = toggleInventory; // // called by 'OverlayController' after being called by 'Interactable'
        setItemNeeded = setItem;

        canvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            toggleInventory();
        }
    }

    // toggles the visibility
    private void toggleInventory()
    {
        if (canvas.activeSelf == true)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
            itemSpawner.showItems(itemList, itemCount); // what calls each icon to show items in inventory
        }
    }

    

    // called by collectable, receives the item type
    private void addToInventory(Items item)
    {

        // searches for each of the inventory slots
        for (int i = 0; i < itemList.Length; i++)
        {

            // checks if there is and empty space or the same item
            if (itemList[i] == Items.empty || itemList[i] == item) 
            {

                // replaces the space with the item received and adds to the stack
                itemList[i] = item; 
                switch (item) 
                {
                    case Items.pistolAmmo:
                        itemCount[i] += 6;
                        break;

                    case Items.syringe:
                        itemCount[i] += 1;
                        break;

                    case Items.keyDoor1: // door key shouldnt be stackable as it has multiple uses
                        //itemCount[i] += 1;
                        break;

                    default:
                        Debug.Log("Invalid Item");
                        break;
                }
                break; 
            }
        }        
    }

    // called by 'ShowIcon' when the button is pressed
    private void UseItem(Items item)
    {
        if (_itemNeeded != Items.empty)
        {
            if (item == _itemNeeded)
            {
                Debug.Log("Correct item"); // do something
                Manager.currentInteractionObj.SendMessage("SetCompleted");
            }
            else
            {
                Instantiate(textPopup);
            }
         
            _itemNeeded = Items.empty;
            toggleInventory();
            
        }
        
        //this._itemNeeded = item;
        //Debug.Log($"Item needed: {_itemNeeded}");
    }

    private void setItem(Items item)
    {
        this._itemNeeded = item;
    }
}
