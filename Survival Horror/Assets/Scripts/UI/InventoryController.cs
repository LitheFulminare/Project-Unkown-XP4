using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour, IDragHandler
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

    [SerializeField] ItemInspector itemInspector;

    private void Start()
    {
        // it wont work, it inventory would appear behind everything
        //Canvas cv = canvas.GetComponent<Canvas>();
        //cv.worldCamera = Camera.main;

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

        // temporary way to equip the weapon
        // will be handled by another UI script in the future
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (CheckIfPlayerHasItem(Items.pistol))
            {
                EquipItem.equipItem(Items.pistol);            
            }
            else
            {
                Debug.Log("Player doesnt have pistol");
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canvas.activeSelf)
            {
                ItemInspector.inspectItem(Items.pistol);

                GameObject bg = GameObject.Find("Background");
                Image bgImage = bg.GetComponent<Image>();

                if (ItemInspector.isInspecting)
                {
                    if (bgImage != null)
                    {
                        bgImage.color = new Color32(60, 60, 60, 255);
                        ShowIcon.toggleButton();
                        //bgImage.color = Color.grey;
                    }
                }
                else
                {                   
                    if (bgImage != null)
                    {
                        bgImage.color = Color.white;
                        ShowIcon.toggleButton();
                    }
                }
                
            }          
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
            PlayerVars.BlockPlayer(false);
            canvas.SetActive(false);
        }
        else if (!PlayerVars.playerBlocked)
        {            
            canvas.SetActive(true);
            itemSpawner.showItems(itemList, itemCount); // what calls each icon to show items in inventory
            PlayerVars.BlockPlayer(true);
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
                    case Items.pistolAmmo: itemCount[i] += 6; break;

                    case Items.syringe: itemCount[i] += 1; break;

                    case Items.pistol: break; // pistol's stack will show ammo instead, this is handled by 'ShowIcon' in 'ChangeText()' func

                    case Items.keyDoor1: break;// door key shouldnt be stackable as it has multiple uses

                    default: Debug.Log("Invalid Item"); break;
                }
                break; 
            }
        }        
    }

    // currently being used by pistol, is only called if the player has pistol ammo
    public int RetrieveItem(Items itemRequested, int quantityRequested)
    {
        // what will be sent to the player
        int quantityRetrieved = 0;

        // goes through each slot to see which one has the item needed
        for (int i = 0; i < itemList.Length; i++)
        {
            // when the item is found
            if (itemList[i] == itemRequested)
            {
                // if the player has enough items
                if (itemCount[i] >= quantityRequested)
                {
                    // subtracts from the inventory what the player needs
                    itemCount[i] -= quantityRequested;

                    // sends to the player what they requested
                    quantityRetrieved = quantityRequested;

                    // removes the item from the list if there's none of it left
                    if (itemCount[i] == 0)
                    {
                        itemList[i] = Items.empty;
                    }

                    // breaks to avoid going through the for loop unnecessarily
                    break;
                }

                // if the player doesn't have enough items
                else
                {
                    // sends everything to the player and removes the item from the list
                    quantityRetrieved = itemCount[i];
                    itemList[i] = Items.empty;
                    itemCount[i] = 0;

                    // breaks to avoid going through the for loop unnecessarily
                    break;
                }
            }
        }

        return quantityRetrieved;
    }

    // goes through each inventory slot searching for the item, returns true if found
    public bool CheckIfPlayerHasItem(Items item)
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

    public void OnDrag(PointerEventData eventData)
    {
        itemInspector.RotateItem(new Vector3(eventData.delta.y, -eventData.delta.x));
    }
}
