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

    // has to receive item to check, return quantity and a bool
    // or, can be called multiple times and depending on how many times it's true the qnt can be calculated
    //public delegate bool InventoryItems()

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

    // called by SceneChanger right before loading the new scene
    // sends inventory data to PlayerVars
    public void SaveInventory()
    {
        PlayerVars.itemList = itemList;
        PlayerVars.itemCount = itemCount;
    }

    public void LoadInventory(Items[] itemList, int[] itemCount)
    {
        this.itemList = itemList;
        this.itemCount = itemCount;
    }

    // toggles the visibility and blocks player movement
    private void toggleInventory()
    {
        if (canvas.activeSelf == true)
        {
            canvas.SetActive(false);
            PlayerVars.BlockMovement(false);
        }
        else
        {
            canvas.SetActive(true);
            itemSpawner.showItems(itemList, itemCount); // what calls each icon to show items in inventory
            PlayerVars.BlockMovement(true);
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

    // currently being used by pistol, is only called if the player has pistol ammo
    private void RemoveItem()
    {

    }

    // goes through each inventory slot searching for the item, returns true if found
    private bool CheckIfPlayerHasItem(Items item)
    {     
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    // called by 'ShowIcon' when the button is pressed
    private void UseItem(Items item)
    {
        if (_itemNeeded != Items.empty)
        {
            if (item == _itemNeeded)
            {
                Manager.currentInteractionObj.SendMessage("SetCompleted");
            }
            else
            {
                Instantiate(textPopup);
            }
         
            _itemNeeded = Items.empty;
            toggleInventory();
            
        }
    }

    // makes the InventoryController "aware" of what item the puzzle requires
    private void setItem(Items item)
    {
        this._itemNeeded = item;
    }
}
