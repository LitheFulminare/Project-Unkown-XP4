using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour, IDragHandler
{
    public delegate void ItemReceiver(CollectableSO item);
    public static ItemReceiver itemReceiver; // used to add item to inventory
    public static ItemReceiver useItem; // used to complete a puzzle/interaction
    public static ItemReceiver setItemNeeded; // self explanatory

    public delegate void ToggleInventory();
    public static ToggleInventory callInventory;

    // has to receive item to check, return quantity and a bool
    // or, can be called multiple times and depending on how many times it's true the qnt can be calculated
    //public delegate bool InventoryItems()

    public CollectableSO[] itemList = new CollectableSO[6]; // list of the items
    public int[] itemCount = new int[6]; // list of how many items the player has
    // the document list will be stored directly into 'PlayerVars'

    public GameObject canvas;

    public ItemSpawner itemSpawner; // what calls each icon to show items in inventory

    private CollectableSO _itemNeeded; // set when the inventory is show because of a puzzle/interaction

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
        //if(Input.GetKeyDown(KeyCode.E))
        //{
        //    if (CheckIfPlayerHasItem(""))
        //    {
        //        EquipItem.equipItem(Items.pistol);            
        //    }
        //    else
        //    {
        //        Debug.Log("Player doesnt have pistol");
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canvas.activeSelf)
            {
                //ItemInspector.inspectItem(Items.pistol);

                GameObject bg = GameObject.Find("Background");
                Image bgImage = bg.GetComponent<Image>();

                //if (ItemInspector.isInspecting)
                //{
                //    if (bgImage != null)
                //    {
                //        bgImage.color = new Color32(60, 60, 60, 255);
                //        ShowIcon.toggleButton();
                //        //bgImage.color = Color.grey;
                //    }
                //}
                //else
                //{                   
                //    if (bgImage != null)
                //    {
                //        bgImage.color = Color.white;
                //        ShowIcon.toggleButton();
                //    }
                //}
                
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

    public void LoadInventory(CollectableSO[] itemList, int[] itemCount)
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
            TabManager.ShowInventoryTab();
            PlayerVars.BlockPlayer(true);
        }
    }

    // called by 'Collected' method  on 'Collectable'
    private void addToInventory(CollectableSO collectedItem)
    {
        // searches for each of the inventory slots
        for (int i = 0; i < itemList.Length; i++)
        {
            // checks if there is and empty space or the same item
            if (itemList[i] == collectedItem || itemList[i] == null) 
            {
                itemList[i] = collectedItem; 
                
                // need to test if this check is really necessary
                if (collectedItem.stack != 0)
                {
                    itemCount[i] += collectedItem.stack;
                }
                break; 
            }
        }        
    }

    // used to remove item from inventory
    public int RetrieveItem(CollectableSO itemRequested, int quantityRequested)
    {
        // what will be sent to the player
        int quantityRetrieved = 0;

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == itemRequested)
            {
                // if the player has (more than) enough
                if (itemCount[i] >= quantityRequested)
                {
                    itemCount[i] -= quantityRequested;

                    quantityRetrieved = quantityRequested;

                    if (itemCount[i] == 0)
                    {
                        itemList[i] = null;
                    }
                }

                // if the player doesnt have enough, just send everything and thats it
                else
                {                  
                    quantityRetrieved = itemCount[i];
                    itemList[i] = null;
                    itemCount[i] = 0;
                }

                break;
            }
        }

        return quantityRetrieved;
    }

    // goes through each inventory slot searching for the item, returns true if found
    public bool CheckIfPlayerHasItem(CollectableSO requestedItem)
    {     
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == requestedItem)
            {
                return true;
            }
        }
        return false;
    }

    // called by 'ShowIcon' when the button is pressed
    private void UseItem(CollectableSO selectedItem)
    {
        // try to get BustInteractable

        if (_itemNeeded != null)
        {
            // I could call the method UsedItem and return a bool. If false, instantiate textPopup
            if (selectedItem.ingameName.Contains("bust") == _itemNeeded.ingameName.Contains("bust"))
            {
                //Manager.currentInteractionObj.SendMessage("SuitableItem"); // this right here would be ideal
                Manager.currentInteractionObj.SendMessage("UsedItem", selectedItem);
                RetrieveItem(selectedItem, 0);
            }
            else
            {
                Instantiate(textPopup);
            }
         
            _itemNeeded = null;
            toggleInventory();
        }
    }

    // makes the InventoryController "aware" of what item the puzzle requires
    private void setItem(CollectableSO item)
    {
        this._itemNeeded = item;
    }

    // used to inspect an item
    public void OnDrag(PointerEventData eventData)
    {
        //itemInspector.RotateItem(new Vector3(eventData.delta.y, -eventData.delta.x));
    }
}
